using TileShop.Domain.Entities;

namespace TileShop.Domain.Repositories;

public interface IOrderDetailsRepository : IBaseRepository<OrderDetails>
{
    Task CreateRangeAsync(List<OrderDetails> orderDetails);
}
