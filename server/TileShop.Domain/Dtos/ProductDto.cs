using TileShop.Domain.Entities;

namespace TileShop.Domain.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public double Discount { get; set; } = 0;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public double AverageRating { get; set; }

    public CategoryDto Category { get; set; }
    public List<Feature> Features { get; set; }
    public List<Review> Reviews { get; set; }
}
