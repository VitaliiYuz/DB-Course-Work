using TileShop.Domain.Entities;

namespace TileShop.Domain.Repositories;

public interface IBasketRepository : IBaseRepository<Basket>
{
    Task<Basket?> GetByUserIdAsync(int userId);
}
