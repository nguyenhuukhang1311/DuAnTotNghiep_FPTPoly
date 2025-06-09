using MenShop_Assignment.Extensions;

namespace MenShop_Assignment.Models.OrderModels.OrderReponse
{
    public class PaymentDiscountDto
    {
        public string? CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DiscountType Type { get; set; }
    }

}
