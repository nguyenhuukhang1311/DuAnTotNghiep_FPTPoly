using MenShop_Assignment.Datas;
using MenShop_Assignment.Models;

namespace MenShop_Assignment.Mapper
{
    public class CustomerAddressMapper
    {
        private readonly ApplicationDbContext _context;
        public CustomerAddressMapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public CustomerAddressViewModel ToCustomerAddressView(CustomerAddress address)
        {
            return new CustomerAddressViewModel
            {
                CustomerName = address.Customer?.UserName,
                Address=address.Address,
                
            };
        }
    }
}
