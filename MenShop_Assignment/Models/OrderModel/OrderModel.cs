namespace MenShop_Assignment.Models.OrderModel
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string? CustomerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? Total { get; set; }
        public string? Status { get; set; }
        public List<OrderDetailModel> Details { get; set; } = new();
    }
}
