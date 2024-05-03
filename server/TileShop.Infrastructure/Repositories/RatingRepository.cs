using TileShop.Domain.Entities;
using TileShop.Domain.Repositories;

namespace TileShop.Infrastructure.Repositories;

public class RatingRepository : BaseRepository<Rating>, IRatingRepository
{
    public RatingRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}
