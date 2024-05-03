namespace TileShop.Domain.Entities;

public class Feature
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;

    public virtual List<FeatureValue> Values { get; set; } = new();
}
