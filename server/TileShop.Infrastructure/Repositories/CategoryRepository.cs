using Microsoft.EntityFrameworkCore;
using TileShop.Domain.Entities;
using TileShop.Domain.Repositories;

namespace TileShop.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context)
            :base(context) { }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await EntitySet.Where(x => x.ParentCategoryId == null)
                .AsNoTracking()
                .Include(x => x.ChildCategories)
                .ToListAsync();

            return categories;
        }
    }
}
