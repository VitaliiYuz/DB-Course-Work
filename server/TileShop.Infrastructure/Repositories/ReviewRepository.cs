using Microsoft.EntityFrameworkCore;
using TileShop.Domain.Entities;
using TileShop.Domain.Repositories;

namespace TileShop.Infrastructure.Repositories;

public class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Review>> GetByCreatedDate(int productId)
    {
        var reviews = await EntitySet
            .AsNoTracking()
            .Where(x => x.ProductId == productId)
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync();
        return reviews;
    }

    public async Task<bool> IsReviewExist(int productId, int userId)
    {
        var isExist = await EntitySet.AnyAsync(x => x.ProductId == productId && x.UserId == userId);
        return isExist;
    }
}
