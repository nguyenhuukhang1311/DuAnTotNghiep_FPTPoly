using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.OrderModels.OrderReponse;

namespace MenShop_Assignment.Mapper.MapperOrder
{
    public class OrderMapper
    {
        public OrderResponseDto ToOrderResponseDto(Order order)
        {
            return new OrderResponseDto
            {
                OrderId = order.OrderId,

                CustomerId = order.CustomerId,
                CustomerName = order.Customer?.UserName,

                EmployeeId = order.EmployeeId,
                EmployeeName = order.Employee?.UserName,

                ShipperId = order.ShipperId,
                ShipperName = order.Shipper?.UserName,

                CreatedDate = order.CreatedDate,
                CompletedDate = order.CompletedDate,
                PaidDate = order.PaidDate,
                Status = order.Status,
                IsOnline = order.IsOnline,
                Total = order.Total,

                Details = order.Details?.Select(ToOrderDetailDto).ToList() ?? new(),
                Payments = order.Payments?.Select(ToPaymentDto).ToList() ?? new()
            };
        }

        private OrderDetailResponseDto ToOrderDetailDto(OrderDetail detail)
        {
            return new OrderDetailResponseDto
            {
                ProductDetailId = detail.ProductDetailId,
                Quantity = detail.Quantity ?? 0,
                Price = detail.Price ?? 0
            };
        }

        public PaymentResponseDto ToPaymentDto(Payment payment)
        {
            return new PaymentResponseDto
            {
                PaymentId = payment.PaymentId,
                //OrderId = payment.OrderId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                Method = payment.Method,
                Status = payment.Status,
                TransactionCode = payment.TransactionCode,
                PaymentProvider = payment.PaymentProvider,
                Notes = payment.Notes,
                StaffId = payment.StaffId,
                Discounts = payment.Discounts?.Select(ToPaymentDiscountDto).ToList() ?? new()
            };
        }

        private PaymentDiscountDto ToPaymentDiscountDto(PaymentDiscount discount)
        {
            return new PaymentDiscountDto
            {
                //DiscountId = discount.DiscountId,
                //PaymentId = discount.PaymentId,
                CouponCode = discount.CouponCode,
                DiscountAmount = discount.DiscountAmount,
                DiscountPercentage = discount.DiscountPercentage,
                Type = discount.Type
            };
        }
    }

}
