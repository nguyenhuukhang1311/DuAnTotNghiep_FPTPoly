using MenShop_Assignment.Extensions;

namespace MenShop_Assignment.Models.Payment
{
    public class CreatePaymentDiscountDto
    {
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DiscountType Type { get; set; }

    }
}
