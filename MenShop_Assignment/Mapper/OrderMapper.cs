using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;
using MenShop_Assignment.Models.OrderModel;

namespace MenShop_Assignment.Mapper
{
    public class OrderMapper 
    {
        public OrderCustomerModel MapToDTO(Order order)
        {
            if (order == null) return null;

            return new OrderCustomerModel
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
                //Details = order.Details?.Select(MapOrderDetailToDTO).ToList()
            };
        }

        public List<OrderCustomerModel> MapToDTO(List<Order> orders)
        {
            if (orders == null || !orders.Any()) return new List<OrderCustomerModel>();

            return orders.Select(MapToDTO).ToList();
        }

        public OrderDetailCustomerModel MapOrderDetailToDTO(OrderDetail orderDetail)
        {
            if (orderDetail == null) return null;

            return new OrderDetailCustomerModel
            {
                ProductDetailId = orderDetail.ProductDetailId,
                ProductName = orderDetail.ProductDetail?.Product?.ProductName,
                SizeName = orderDetail.ProductDetail?.Size?.Name,
                ColorName = orderDetail.ProductDetail?.Color?.Name,
                FabricName = orderDetail.ProductDetail?.Fabric?.Name,
                Quantity = orderDetail.Quantity,
                Price = orderDetail.Price,
                TotalPrice = (orderDetail.Quantity ?? 0) * (orderDetail.Price ?? 0)
            };
        }
    }
}
