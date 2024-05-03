using TileShop.Domain.Entities;

namespace TileShop.API.Products.Requests;

public class UpdateProductRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public double Discount { get; set; } = 0;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
