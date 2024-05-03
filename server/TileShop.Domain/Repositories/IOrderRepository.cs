using TileShop.Domain.Entities;

namespace TileShop.Domain.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Dictionary<int, int> GetOrderCountForCurrentYearByMonth();
    Dictionary<int, decimal> GetAverageOrderForCurrentYearByMonth();
    Dictionary<int, decimal> GetTotalOrderInCurrentYearForMonth();
    Task<List<Order>> GetOrdersForLast30DaysAsync();
    Task<List<Order>> GetUserOrdersAsync(int userId);
}
