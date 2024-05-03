namespace TileShop.API.Reviews.Requests;

public class CreateReviewRequest
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string Comment { get; set; } = string.Empty;
}
