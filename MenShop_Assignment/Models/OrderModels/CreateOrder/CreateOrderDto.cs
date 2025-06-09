using MenShop_Assignment.Extensions;

namespace MenShop_Assignment.Models.OrderModels.CreateOrder
{
    public class CreateOrderDto
    {
        public string? CustomerId { get; set; }
        public string? EmployeeId { get; set; }
        public string? ShipperId { get; set; }
        public bool IsOnline { get; set; }
        public List<CreateOrderDetailDto> Details { get; set; } = new();
    }

}
