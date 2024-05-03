namespace TileShop.API.Reviews.Requests;

public class UpdateReviewRequest
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string Comment { get; set; } = string.Empty;
}
