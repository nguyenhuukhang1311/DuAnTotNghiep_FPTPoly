using MenShop_Assignment.Extensions;

namespace MenShop_Assignment.Models.OrderModel
{
    public class OrderSearchCustomerModel
    {
        public string? CustomerId { get; set; }
        public OrderStatus? Status { get; set; }
    }
}
