using MenShop_Assignment.Extensions;

namespace MenShop_Assignment.Models.OrderModels.OrderReponse
{
    public class PaymentResponseDto
    {
        public string PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public string? TransactionCode { get; set; }
        public string? PaymentProvider { get; set; }
        public string? Notes { get; set; }

        public string? StaffId { get; set; }
        public List<PaymentDiscountDto> Discounts { get; set; } = new();
    }

}
