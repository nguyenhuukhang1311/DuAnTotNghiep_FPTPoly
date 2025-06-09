using MenShop_Assignment.Extensions;

namespace MenShop_Assignment.Models.OrderModels.OrderReponse
{
    public class OrderResponseDto
    {
        public string OrderId { get; set; }

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
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<OrderDetailResponseDto> Details { get; set; } = new();
        public List<PaymentResponseDto> Payments { get; set; } = new();
    }


}
