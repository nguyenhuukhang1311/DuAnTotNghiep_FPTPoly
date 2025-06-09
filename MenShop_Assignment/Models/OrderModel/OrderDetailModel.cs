namespace MenShop_Assignment.Models.OrderModel
{
    public class OrderDetailModel
    {
        public int ProductDetailId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? ProductName { get; set; }
    }
}
