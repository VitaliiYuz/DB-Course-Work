using TileShop.API.Products.Responses;

namespace TileShop.API.Order.Responses
{
    public class OrderDetailsResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductResponse Product { get; set; }
    }
}
