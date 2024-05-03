namespace TileShop.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? ParentCategoryId { get; set; }

    public virtual Category? ParentCategory { get; set; }
    public virtual List<Category> ChildCategories { get; set; } = new();
}
