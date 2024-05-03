namespace TileShop.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public double Discount { get; set; } = 0;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public double AverageRating { get; set; }

    public virtual Category? Category { get; set; }
    public virtual List<Feature> Features { get; set; } = new();
    public virtual List<Review> Reviews { get; set; } = new();
}
