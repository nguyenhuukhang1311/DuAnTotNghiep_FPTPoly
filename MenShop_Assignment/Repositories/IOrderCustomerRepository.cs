using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;
using MenShop_Assignment.Models.OrderModel;

namespace MenShop_Assignment.Repositories
{
    public interface IOrderCustomerRepository
    {
        Task<List<Order>> GetPendingOrdersAsync();
        Task<Order?> GetOrderWithDetailsAsync(int orderId);
        Task<List<Order>> GetOrdersByCustomerIdAsync(string customerId);
        Task<List<Order>> SearchOrdersAsync(OrderSearchCustomerModel searchDto);
        Task<bool> CancelOrderAsync(int orderId);
        Task<bool> CanCancelOrderAsync(int orderId);
        Task UpdateStorageQuantityAsync(List<OrderDetail> orderDetails); // Thêm method này
        Task SaveChangesAsync();
    }
}
