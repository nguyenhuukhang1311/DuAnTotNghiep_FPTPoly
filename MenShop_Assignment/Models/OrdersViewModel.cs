using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;

namespace MenShop_Assignment.Models
{
    public class OrdersViewModel
    {
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public string? EmployeeName { get; set; }
        public string? ShipperName { get; set; }
        public User? Shipper { get; set; }
        public User? Employee { get; set; }
        public User? Customer { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public OrderStatus? Status { get; set; }
        public bool? IsOnline { get; set; }
        public decimal? Total { get; set; }
        public string? Address { get; set; }
        public ICollection<OrderDetail>? Details { get; set; }
        // Thêm quan hệ với Payment
        public ICollection<Payment>? Payments { get; set; }
    }
}
