namespace TileShop.API.Categories.Requests;

public class UpdateCategoryRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? ParentCategoryId { get; set; }
}
