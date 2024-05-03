namespace TileShop.Domain.Entities;

public class FeatureValue
{
    public int Id { get; set; }
    public int FeatureId { get; set; }
    public string Value { get; set; } = string.Empty;
}
