namespace TileShop.Domain.Entities;

public class Basket
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public List<BasketDetails> Items { get; set;} = new();
}
