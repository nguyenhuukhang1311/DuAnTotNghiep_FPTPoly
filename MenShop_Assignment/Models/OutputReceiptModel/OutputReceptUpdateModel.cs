using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;

namespace MenShop_Assignment.Models.OutputReceipt
{
    public class OutputReceptUpdateModel
    {
        public DateTime? ConfirmedDate { get; set; }
        public OrderStatus? Status { get; set; }
    }
}
