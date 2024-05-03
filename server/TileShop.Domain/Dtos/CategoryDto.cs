namespace TileShop.Domain.Dtos;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? ParentCategoryId { get; set; }
    public virtual List<CategoryDto> ChildCategories { get; set; } = new();
}
