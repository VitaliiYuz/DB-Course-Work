using Microsoft.EntityFrameworkCore;
using TileShop.Domain.Entities;

namespace TileShop.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<User> User { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Feature> Feature { get; set; }
    public DbSet<FeatureValue> FeatureValue { get; set; }
    public DbSet<Basket> Basket { get; set; }
    public DbSet<BasketDetails> BasketDetails { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<Rating> Rating { get; set; }
    public DbSet<Review> Review { get; set; }
}
