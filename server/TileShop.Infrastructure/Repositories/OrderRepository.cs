using Microsoft.EntityFrameworkCore;
using TileShop.Domain.Entities;
using TileShop.Domain.Repositories;

namespace TileShop.Infrastructure.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context)
        : base(context)
    {
        
    }

    public int GetOrderCountInCurrentYearForMonth(int month)
    {
        var currentYear = DateTime.UtcNow.Year;

        return EntitySet
            .Where(x => x.CreatedDate.Month == month && x.CreatedDate.Year == currentYear)
            .Count();
    }

    public Dictionary<int, int> GetOrderCountForCurrentYearByMonth()
    {
        var currentYear = DateTime.UtcNow.Year;

        var orderCountsByMonth = EntitySet
            .Where(x => x.CreatedDate.Year == currentYear)
            .GroupBy(x => x.CreatedDate.Month)
            .Select(g => new { Month = g.Key, OrderCount = g.Count() })
            .ToDictionary(x => x.Month, x => x.OrderCount);

        return orderCountsByMonth;
    }

    public Dictionary<int, decimal> GetAverageOrderForCurrentYearByMonth()
    {
        var currentYear = DateTime.UtcNow.Year;

        var orderCountsByMonth = EntitySet
            .Where(x => x.CreatedDate.Year == currentYear)
            .GroupBy(x => x.CreatedDate.Month)
            .Select(g => new { Month = g.Key, AverageOrder = g.Average(x => x.TotalPrice) })
            .ToDictionary(x => x.Month, x => x.AverageOrder);

        return orderCountsByMonth;
    }

    public Dictionary<int, decimal> GetTotalOrderInCurrentYearForMonth()
    {
        var currentYear = DateTime.UtcNow.Year;

        var totalOrder = EntitySet
            .Where(x => x.CreatedDate.Year == currentYear)
            .GroupBy(x => x.CreatedDate.Month)
            .Select(g => new { Month = g.Key, SumOrder = g.Sum(x => x.TotalPrice) })
            .ToDictionary(x => x.Month, x => x.SumOrder);
        return totalOrder;
    }

    public async Task<List<Order>> GetOrdersForLast30DaysAsync()
    {
        var startDate = DateTime.Today.AddDays(-30);
        var endDate = DateTime.Today;
        return await EntitySet
            .Include(x => x.Details)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Category)
            .Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate)
            .ToListAsync();
    }

    public async Task<List<Order>> GetUserOrdersAsync(int userId)
    {
        var orders = await EntitySet
            .Where(order => order.UserId == userId)
            .Include(order => order.Details)
            .ThenInclude(details => details.Product)
            .OrderByDescending(order => order.CreatedDate)
            .ToListAsync();

        return orders;
    }
}
