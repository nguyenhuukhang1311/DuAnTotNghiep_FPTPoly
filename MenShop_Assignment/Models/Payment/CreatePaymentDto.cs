using MenShop_Assignment.Extensions;
using MenShop_Assignment.Models.OrderModels.CreateOrder;

namespace MenShop_Assignment.Models.Payment
{
    public class CreatePaymentDto
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public string? TransactionCode { get; set; }
        public string? PaymentProvider { get; set; }
        public string? Notes { get; set; }

        public ICollection<CreatePaymentDiscountDto>? Discounts { get; set; }
    }
}
