namespace MenShop_Assignment.Models.OrderModel
{
    public class OrderDetailCustomerModel
    {
        public int ProductDetailId { get; set; }
        public string? ProductName { get; set; }
        public string? SizeName { get; set; }
        public string? ColorName { get; set; }
        public string? FabricName { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
