using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;
using MenShop_Assignment.Models.OrderModel;

namespace MenShop_Assignment.Repositories
{
    public interface IOrderCustomerRepository
    {
        Task<List<Order>> GetPendingOrdersAsync();
        Task<Order?> GetOrderWithDetailsAsync(string orderId);
        Task<List<Order>> GetOrdersByCustomerIdAsync(string customerId);
        Task<List<Order>> SearchOrdersAsync(OrderSearchCustomerModel searchDto);
        Task<bool> CancelOrderAsync(string orderId);
        Task<bool> CanCancelOrderAsync(string orderId);
        Task UpdateStorageQuantityAsync(List<OrderDetail> orderDetails);
        Task SaveChangesAsync();
    }
}
