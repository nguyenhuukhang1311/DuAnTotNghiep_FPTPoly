using MenShop_Assignment.Models.Momo;
using MenShop_Assignment.Models.OrderModels;
using MenShop_Assignment.Models.OrderModels.CreateOrder;

namespace MenShop_Assignment.Services.Momo
{
    public interface IMomoServices
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInforDTO model);
        MomoExecuteResponseModel PaymentExecute(IQueryCollection colection);
    }
}
