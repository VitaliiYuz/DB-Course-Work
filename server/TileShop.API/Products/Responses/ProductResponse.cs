using TileShop.API.Categories.Responses;

namespace TileShop.API.Products.Responses;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Discount { get; set; }
    public decimal Price { get; set; }
    public double AverageRating { get; set; }
    public CategoryResponse Category { get; set; }

}
