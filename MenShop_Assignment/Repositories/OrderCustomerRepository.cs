using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models.OrderModel;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Repositories
{
    public class OrderCustomerRepository : IOrderCustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderCustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetPendingOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Details)
                    .ThenInclude(d => d.ProductDetail)
                .Where(o => o.Status == OrderStatus.Pending)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<Order?> GetOrderWithDetailsAsync(string orderId)
        {
            return await _context.Orders
                .Include(o => o.Details)
                    .ThenInclude(d => d.ProductDetail)
                    .AsSplitQuery()
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        //Order Customer 

        public async Task<List<Order>> GetOrdersByCustomerIdAsync(string? customerId)
        {
            if(customerId == null)
            {
                return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Shipper)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Product)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Size)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Color)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Fabric)
                .OrderByDescending(o => o.CreatedDate)
                .AsSplitQuery()
                .ToListAsync();
            }
            else
            {
                return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Shipper)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Product)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Size)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Color)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Fabric)
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.CreatedDate)
                .AsSplitQuery()
                .ToListAsync();
            }
            
        }

        public async Task<Order?> GetOrderByIdAsync(string orderId)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Shipper)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Product)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Size)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Color)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Fabric)
                        .AsSplitQuery()
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<List<Order>> SearchOrdersAsync(OrderSearchCustomerModel searchDto)
        {
            var query = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Shipper)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Product)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Size)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Color)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ProductDetail)
                        .ThenInclude(pd => pd.Fabric)
                        .AsSplitQuery()
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.CustomerId))
            {
                query = query.Where(o => o.CustomerId == searchDto.CustomerId);
            }

            if (searchDto.Status.HasValue)
            {
                query = query.Where(o => o.Status == searchDto.Status.Value);
            }

            return await query.OrderByDescending(o => o.CreatedDate).ToListAsync();
        }

        public async Task<bool> CanCancelOrderAsync(string orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            return order.Status == OrderStatus.Pending || order.Status == OrderStatus.Confirmed;
        }

        public async Task<bool> CancelOrderAsync(string orderId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var order = await _context.Orders
                    .Include(o => o.Details)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(o => o.OrderId == orderId);

                if (order == null) return false;

                if (!(order.Status == OrderStatus.Pending || order.Status == OrderStatus.Confirmed))
                {
                    return false;
                }

                order.Status = OrderStatus.Cancelled;

                if (order.Details != null && order.Details.Any())
                {
                    await UpdateStorageQuantityAsync(order.Details.ToList());
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task UpdateStorageQuantityAsync(List<OrderDetail> orderDetails)
        {
            foreach (var detail in orderDetails)
            {
                var storageDetail = await _context.StorageDetails
                    .FirstOrDefaultAsync(sd => sd.ProductDetailId == detail.ProductDetailId);

                if (storageDetail != null && detail.Quantity.HasValue)
                {
                    storageDetail.Quantity = (storageDetail.Quantity ?? 0) + detail.Quantity.Value;
                }
            }
        }
    }
}
