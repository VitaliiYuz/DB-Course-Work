namespace TileShop.API.Order.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetailsResponse> Details { get; set; }
    }
}
