using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.Momo;
using MenShop_Assignment.Models.Payment;
using MenShop_Assignment.Models.OrderModels.OrderReponse;

namespace MenShop_Assignment.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> AddPaymentToOrderAsync(string orderId, CreatePaymentDto dto);
        //Task<(OrderResponseDto Order, MomoCreatePaymentResponseModel Payment)> CreateOrderAndPayAsync(CreateOrderDto dto);
        Task<PaymentResponseDto> AddCodPaymentAsync(string orderId);

    }
}
