namespace TileShop.Domain.Dtos;

public class BasketDetailsDto
{
    public int Id { get; set; }
    public int BasketId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public ProductDto Product { get; set; }
}
