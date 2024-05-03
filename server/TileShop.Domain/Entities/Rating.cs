namespace TileShop.Domain.Entities;

public class Rating
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public double Score { get; set; }
}
