using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;

namespace MenShop_Assignment.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly OrdersMapper _shipperordersMapper;
        private readonly CustomerAddressMapper _customeraddressMapper;
        public OrderRepository(ApplicationDbContext context, OrdersMapper shipperorderMapper, CustomerAddressMapper customeraddressMapper)
        {
            _context = context;
            _shipperordersMapper = shipperorderMapper;
            _customeraddressMapper = customeraddressMapper;
        }
        public async Task<List<OrdersViewModel>> GetAllOrdersAsync()
        {
            return await _context.Orders.Select(x => _shipperordersMapper.ToShipperOrdersView(x)).ToListAsync();
        }

        public async Task<List<OrdersViewModel>> GetAllOnlineOrdersAsync()
        {
            return await _context.Orders
                 .Where(x => x.IsOnline == true)
                 .Select(x => _shipperordersMapper.ToShipperOrdersView(x))
                 .ToListAsync();
        }
        public async Task<List<OrdersViewModel>> GetOrdersByShipperId(string shipperId)
        {
            return await _context.Orders
                 .Where(x => x.ShipperId == shipperId)
                 .Select(x => _shipperordersMapper.ToShipperOrdersView(x))
                 .ToListAsync();
        }
        public async Task ShipperAcceptOrderByOrderId(int orderId,string shipperId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
            if (order != null)
            {
                order.ShipperId = shipperId; 
                order.Status = (OrderStatus)1; 
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
        public async Task CompletedOrderStatus(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(x => x.OrderId == orderId);
            order.Status = (OrderStatus)2;
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<List<OrdersViewModel>> GetOrdersByDistrict(string district)
        {
            var validDistricts = new[] { "Liên Chiểu", "Hải Châu", "Ngũ Hành Sơn", "Sơn Trà", "Cẩm Lệ", "Thanh Khê" };
            
            if (!validDistricts.Contains(district))
            {
                throw new ArgumentException($"Quận/huyện không hợp lệ. Chỉ chấp nhận: {string.Join(", ", validDistricts)}");
            }

            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Shipper)
                .Where(x => x.Address != null && x.Address.ToLower().Contains(district.ToLower()))
                .Select(x => _shipperordersMapper.ToShipperOrdersView(x))
                .ToListAsync();
        }
    }
}
