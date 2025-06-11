using MenShop_Assignment.Extensions;
using MenShop_Assignment.Models;

namespace MenShop_Assignment.Repositories
{
    public interface IOrderRepository
    {
        Task<List<OrdersViewModel>> GetAllOrdersAsync();
        Task<List<OrdersViewModel>> GetAllOnlineOrdersAsync();
        Task<List<OrdersViewModel>> GetOrdersByShipperId(string shipperId);
        Task ShipperAcceptOrderByOrderId(int orderId, string shipperId);
        Task CompletedOrderStatus(int orderId);
        Task<List<OrdersViewModel>> GetOrdersByDistrict(string district);
    }
}
