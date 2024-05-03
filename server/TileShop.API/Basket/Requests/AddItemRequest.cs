namespace TileShop.API.Basket.Requests;

public class AddItemRequest
{
    public int BasketId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; } = 1;
}
