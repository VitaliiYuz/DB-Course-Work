using TileShop.Domain.Dtos;

namespace TileShop.Application.Services.Interfaces;

public interface IOrderService
{
    Dictionary<int, int> GetOrderCountForCurrentYearByMonth();
    Dictionary<int, decimal> GetAverageOrderPrice();
    Dictionary<int, decimal> GetTotalOrder();
    Task<List<OrderDto>> GetOrdersForLast30DaysAsync();
    Task<List<OrderDto>> GetUserOrdersAsync(int userId);
    Task<OrderDto> DoCheckoutAsync(int userId);
}
