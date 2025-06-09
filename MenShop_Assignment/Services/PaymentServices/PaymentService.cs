using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;
using MenShop_Assignment.Mapper.MapperOrder;
using MenShop_Assignment.Models.Momo;
using MenShop_Assignment.Models.Payment;
using MenShop_Assignment.Models.OrderModels.OrderReponse;
using MenShop_Assignment.Repositories.OrderRepositories;
using MenShop_Assignment.Services.Momo;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;
        private readonly OrderMapper _mapper;
        private readonly IMomoServices _momoServices;
        private readonly IOrderRepository _orderRepository;
        public PaymentService(ApplicationDbContext context, OrderMapper orderMapper, IMomoServices momoServices, IOrderRepository orderRepository)
        {
            _context = context;
            _mapper = orderMapper;
            _momoServices = momoServices;
            _orderRepository = orderRepository;
        }
        public async Task<PaymentResponseDto> AddPaymentToOrderAsync(string orderId, CreatePaymentDto dto)
        {
            var order = await _context.Orders
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                throw new Exception("Không tìm thấy đơn hàng.");

            if (dto.Method == PaymentMethod.Cash)
            {
                dto.PaymentDate = DateTime.UtcNow;
                dto.Status = PaymentStatus.Completed;
                dto.PaymentProvider = "Cash";
                dto.TransactionCode = null;
                dto.Notes = "Thanh toán bằng tiền mặt tại quầy";
            }


            var payment = new Payment
            {
                PaymentId = "PM" + Guid.NewGuid().ToString("N").Substring(0, 12),
                OrderId = orderId,
                Amount = dto.Amount,
                PaymentDate = dto.PaymentDate,
                Method = dto.Method,
                Status = dto.Status,
                TransactionCode = dto.TransactionCode,
                PaymentProvider = dto.PaymentProvider,
                Notes = dto.Notes,
                StaffId = order.EmployeeId,
                Discounts = dto.Discounts?.Select(d => new PaymentDiscount
                {
                    CouponCode = d.CouponCode,
                    DiscountAmount = d.DiscountAmount,
                    DiscountPercentage = d.DiscountPercentage,
                    Type = d.Type,
                }).ToList()
            };

            order.Payments.Add(payment);
            //
            if(order.IsOnline == false)
            {
                var totalPaid = order.Payments.Sum(p => p.Amount);
                if (totalPaid > order.Total)
                {
                    throw new Exception("Tổng tiền thanh toán vượt quá số tiền đơn hàng.");
                }
                if (totalPaid == order.Total && payment.Status == PaymentStatus.Completed)
                {
                    order.Status = OrderStatus.Completed;
                    order.CompletedDate = DateTime.UtcNow;
                }

            }
            if(order.IsOnline == true)
            {

            }

            await _context.SaveChangesAsync();

            // Load Discounts để trả về đầy đủ
            await _context.Entry(payment).Collection(p => p.Discounts).LoadAsync();

            return _mapper.ToPaymentDto(payment);
        }




    }

}
