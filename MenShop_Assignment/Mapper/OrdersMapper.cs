using MenShop_Assignment.Datas;
using MenShop_Assignment.Models;

namespace MenShop_Assignment.Mapper
{
    public  class OrdersMapper
    {
        private readonly ApplicationDbContext _context;
        public OrdersMapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public OrdersViewModel ToShipperOrdersView(Order order)
        {
            return new OrdersViewModel
            {
               OrderId=order.OrderId,
               CustomerName=order.Customer?.Id,
               EmployeeName=order.Employee?.Id,
               ShipperName= _context.Users.Where(x => x.Id.Equals(order.ShipperId)).FirstOrDefault().UserName,
               IsOnline =order.IsOnline,
               Status=order.Status,
               Total= order.Total,
               Payments= order.Payments,
               CreatedDate= order.CreatedDate,
               CompletedDate = order.CompletedDate,
               Address=order.Address,
            };
        }
    }
}
