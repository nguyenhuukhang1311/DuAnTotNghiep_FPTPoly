using MenShop_Assignment.Models.OrderModels.CreateOrder;
using MenShop_Assignment.Models.OrderModels.OrderReponse;

namespace MenShop_Assignment.Repositories.OrderRepositories
{
    public interface IOrderRepository
    {
        Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto createProductDTO);
        //Task DeleteOrderAsync(int orderId);
    }
}
