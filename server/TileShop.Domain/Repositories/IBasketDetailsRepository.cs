using TileShop.Domain.Entities;

namespace TileShop.Domain.Repositories;

public interface IBasketDetailsRepository : IBaseRepository<BasketDetails>
{
    Task<BasketDetails?> GetExistDetails(int basketId, int productId);
    Task<List<BasketDetails>> GetDetailsByBasketIdAsync(int id);
    Task DeleteAllBasketItems(int id);
}
