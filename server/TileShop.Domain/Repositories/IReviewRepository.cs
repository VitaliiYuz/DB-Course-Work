using TileShop.Domain.Entities;

namespace TileShop.Domain.Repositories;

public interface IReviewRepository : IBaseRepository<Review>
{
    Task<List<Review>> GetByCreatedDate(int productId);
    Task<bool> IsReviewExist(int productId, int userId);
}
