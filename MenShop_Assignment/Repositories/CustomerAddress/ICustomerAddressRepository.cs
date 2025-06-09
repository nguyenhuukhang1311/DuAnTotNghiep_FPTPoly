using MenShop_Assignment.Models.CustomerModels;

namespace MenShop_Assignment.Repositories.CustomerAddress
{
    public interface ICustomerAddressRepository
    {
        Task<List<CustomerAddressModel>> GetAllAsync();
        Task<List<CustomerAddressModel>> GetByCustomerIdAsync(string customerId);
        Task<CustomerAddressReponse> AddAsync(CreateCustomerAddressDto dto);
        Task<CustomerAddressReponse> UpdateAsync(UpdateCustomerAddressDto dto);
        Task DeleteAsync(int id);
    }

}
