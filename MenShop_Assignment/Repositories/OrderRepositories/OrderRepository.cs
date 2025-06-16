using Azure;
using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;
using MenShop_Assignment.Mapper.MapperOrder;
using MenShop_Assignment.Mapper.MapperProduct;
using MenShop_Assignment.Models.OrderModels.CreateOrder;
using MenShop_Assignment.Models.OrderModels.OrderReponse;
using MenShop_Assignment.Repositories.Carts;
using MenShop_Assignment.Services.PaymentServices;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Repositories.OrderRepositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly OrderMapper _mapper;
        private readonly ICartRepository _cartRepository;

        public OrderRepository(ApplicationDbContext context, OrderMapper orderMapper, ICartRepository cartRepository)
        {
            _context = context;
            _mapper = orderMapper;
            _cartRepository = cartRepository;
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

                bool isOnline = true;
                bool isCOD = !string.IsNullOrWhiteSpace(createProductDTO.ShipperId);

                var order = new Order
                {
                    OrderId = "OD" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
                    CustomerId = createProductDTO.CustomerId,
                    EmployeeId = null,
                    ShipperId = createProductDTO.ShipperId,
                    IsOnline = isOnline,
                    CreatedDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending,
                    Total = total,
                    PaidDate = null, 
                    CompletedDate = null,
                    Details = orderDetails
                };


                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                var cart = await _context.Carts.Include(c => c.Details).FirstOrDefaultAsync(c => c.CustomerId == createProductDTO.CustomerId);

                if (cart != null)
                {
                    var detailsToRemove = cart.Details?.Where(cd => productDetailIds.Contains(cd.ProductDetailId ?? 0)).ToList();

                    if (detailsToRemove != null && detailsToRemove.Any())
                        _context.CartDetails.RemoveRange(detailsToRemove);

                    if (cart.Details == null || cart.Details.Count == detailsToRemove?.Count)
                        _context.Carts.Remove(cart);

                    await _context.SaveChangesAsync();
                }

                var orderWithDetails = await _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Employee)
                    .Include(o => o.Shipper)
                    .Include(o => o.Payments).ThenInclude(p => p.Discounts)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

                if (orderWithDetails == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Không tìm thấy đơn hàng sau khi tạo";
                    return response;
                }

                response = _mapper.ToOrderResponseDto(orderWithDetails);
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
