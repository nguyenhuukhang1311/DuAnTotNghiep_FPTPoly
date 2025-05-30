using MenShop_Assignment.Extensions;

namespace MenShop_Assignment.Models.OrderModel
{
    public class OrderCustomerModel
    {
        public int OrderId { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? ShipperId { get; set; }
        public string? ShipperName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public OrderStatus? Status { get; set; }
        public bool? IsOnline { get; set; }
        public decimal? Total { get; set; }
    }
}
