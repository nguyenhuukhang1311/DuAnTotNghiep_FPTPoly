using Azure;
using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;
using MenShop_Assignment.Mapper.MapperOrder;
using MenShop_Assignment.Mapper.MapperProduct;
using MenShop_Assignment.Models.OrderModels.CreateOrder;
using MenShop_Assignment.Models.OrderModels.OrderReponse;
using MenShop_Assignment.Services.PaymentServices;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Repositories.OrderRepositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly OrderMapper _mapper;

        public OrderRepository(ApplicationDbContext context, OrderMapper orderMapper)
        {
            _context = context;
            _mapper = orderMapper;
        }
        public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto createProductDTO)
        {
            var response = new OrderResponseDto();
            try
            {
                var productDetailIds = createProductDTO.Details.Select(d => d.ProductDetailId).ToList();
                var orderDetails = createProductDTO.Details.Select(d => new OrderDetail
                {
                    ProductDetailId = d.ProductDetailId,
                    Quantity = d.Quantity,
                    Price = d.Price 
                }).ToList();

                decimal total = orderDetails.Sum(d => (d.Price ?? 0) * d.Quantity).GetValueOrDefault();

                bool isOnline = string.IsNullOrWhiteSpace(createProductDTO.EmployeeId);

                var order = new Order
                {
                    OrderId = "OD" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
                    CustomerId = createProductDTO.CustomerId,
                    EmployeeId = isOnline ? null : createProductDTO.EmployeeId, 
                    ShipperId = null, 
                    IsOnline = isOnline,
                    CreatedDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending,
                    Total = total,
                    PaidDate = isOnline ? null : DateTime.UtcNow,
                    CompletedDate = null,
                    Details = orderDetails
                };



                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                var orderWithDetails = await _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Employee)
                    .Include(o => o.Shipper)
                    .Include(o => o.Payments)
                        .ThenInclude(p => p.Discounts)
                    .AsSplitQuery()  
                    .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);


                if (orderWithDetails == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Không tìm thấy đơn hàng sau khi tạo";
                    return response;
                }

                // Convert sang DTO
                response = _mapper.ToOrderResponseDto(order);
                response.IsSuccess = true;
                response.Message = "Tạo hóa đơn thành công";
            }
            catch (DbUpdateException dbEx)
            {
                response.IsSuccess = false;
                response.Message = "Lỗi khi lưu vào CSDL: " + (dbEx.InnerException?.Message ?? dbEx.Message);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

    }
}