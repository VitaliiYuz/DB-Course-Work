namespace TileShop.Domain.Dtos;

public class BasketDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<BasketDetailsDto> Items { get; set; }
}
