using MenShop_Assignment.Models.OrderModel;

namespace MenShop_Assignment.DTO
{
    public class PendingOrdersDto
    {
        public int Count { get; set; }
        public List<OrderCustomerModel> Orders { get; set; } = new();
    }
}
