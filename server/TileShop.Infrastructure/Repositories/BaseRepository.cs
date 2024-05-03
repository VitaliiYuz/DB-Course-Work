using Microsoft.EntityFrameworkCore;
using TileShop.Domain.Repositories;

namespace TileShop.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected ApplicationDbContext Context;
    protected DbSet<TEntity> EntitySet;

    public BaseRepository(ApplicationDbContext context)
    {
        Context = context;
        EntitySet = Context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await EntitySet.FindAsync(id);
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var addedEntry = await EntitySet.AddAsync(entity);
        await Context.SaveChangesAsync();
        return addedEntry.Entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        EntitySet.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await EntitySet.FindAsync(id);
        if (entity is not null)
        {
            EntitySet.Remove(entity);
        }

        await Context.SaveChangesAsync();
    }
}
