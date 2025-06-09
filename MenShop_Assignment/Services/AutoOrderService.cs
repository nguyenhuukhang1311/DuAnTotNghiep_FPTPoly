using MenShop_Assignment.Datas;
using MenShop_Assignment.DTO;
using MenShop_Assignment.Extensions;
using MenShop_Assignment.Models.OrderModel;
using MenShop_Assignment.Repositories;
namespace MenShop_Assignment.Services
{
    public class AutoOrderService : IAutoOrderService
    {
        private readonly IOrderCustomerRepository _orderRepo;
        private readonly IStorageRepository _storageRepo;

        public AutoOrderService(IOrderCustomerRepository orderRepo, IStorageRepository storageRepo)
        {
            _orderRepo = orderRepo;
            _storageRepo = storageRepo;
        }

        public async Task<ApprovalResultDto> ApproveOrderAsync(string orderId)
        {
            var order = await _orderRepo.GetOrderWithDetailsAsync(orderId);

            if (order == null)
                return new ApprovalResultDto { Success = false, OrderId = orderId, Message = "Không tìm thấy đơn hàng" };

            if (order.Status != OrderStatus.Pending)
                return new ApprovalResultDto { Success = false, OrderId = orderId, Message = "Đơn hàng không ở trạng thái chờ xử lý" };
            if (order.IsOnline != true)
                return new ApprovalResultDto { Success = false, OrderId = orderId, Message = "Đơn hàng không là đơn hàng Online" };

            foreach (var detail in order.Details)
            {
                var storage = await _storageRepo.GetByProductIdAsync(detail.ProductDetailId);
                if (storage == null)
                    return new ApprovalResultDto { Success = false, OrderId = orderId, Message = $"Sản phẩm {detail.ProductDetailId} không có trong kho" };

                if (storage.Quantity < detail.Quantity)
                    return new ApprovalResultDto { Success = false, OrderId = orderId, Message = $"Sản phẩm {detail.ProductDetailId} không đủ hàng" };
            }

            foreach (var detail in order.Details)
            {
                var storage = await _storageRepo.GetByProductIdAsync(detail.ProductDetailId);
                storage.Quantity -= detail.Quantity;
            }

            order.Status = OrderStatus.Confirmed;
            order.CompletedDate = DateTime.Now;

            await _orderRepo.SaveChangesAsync();
            await _storageRepo.SaveChangesAsync();

            return new ApprovalResultDto { Success = true, OrderId = orderId, Message = "Duyệt đơn hàng thành công" };
        }

        public async Task<BatchApprovalDto> ApproveAllOrdersAsync()
        {
            var allPendingOrders = await _orderRepo.GetPendingOrdersAsync();

            var onlinePendingOrders = allPendingOrders.Where(o => o.IsOnline == true).ToList();

            var result = new BatchApprovalDto { Total = onlinePendingOrders.Count };

            foreach (var order in onlinePendingOrders)
            {
                var approvalResult = await ApproveOrderAsync(order.OrderId);
                if (approvalResult.Success)
                {
                    result.Approved++;
                    result.ApprovedIds.Add(order.OrderId);
                }
                else
                {
                    result.Failed++;
                    result.FailedOrders.Add(new FailedOrderDto
                    {
                        OrderId = order.OrderId,
                        Error = approvalResult.Message
                    });
                }
            }

            return result;
        }

        public async Task<PendingOrdersDto> GetPendingOrdersAsync()
        {
            var orders = await _orderRepo.GetPendingOrdersAsync();

            return new PendingOrdersDto
            {
                Count = orders.Count,
                Orders = orders.Select(order => new OrderCustomerModel
                {
                    OrderId = order.OrderId,
                    CustomerId = order.CustomerId,
                    CustomerName = order.Customer?.UserName,
                    EmployeeId = order.EmployeeId,
                    EmployeeName = order.Employee?.UserName,
                    ShipperId = order.ShipperId,
                    ShipperName = order.Shipper?.UserName,
                    CreatedDate = order.CreatedDate,
                    CompletedDate = order.CompletedDate,
                    PaidDate = order.PaidDate,
                    Status = order.Status,
                    IsOnline = order.IsOnline,
                    Total = order.Total,
                }).ToList()
            };
        }

        
    }
}