using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.CustomerModels;

namespace MenShop_Assignment.Mapper
{
    public class MapperCustomerAddress
    {
        public CustomerAddress MapToCustomerAddress(CreateCustomerAddressDto dto)
        {
            return new CustomerAddress
            {
                CustomerId = dto.CustomerId,
                Address = dto.Address
            };
        }
        public  CustomerAddressModel MapToCustomerAddressDto(CustomerAddress entity)
        {
            return new CustomerAddressModel
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId ?? string.Empty,
                Address = entity.Address ?? string.Empty
            };
        }
        public CustomerAddressReponse MapToCustomerAddressResponse(CustomerAddress entity, bool success = true, string message = "")
        {
            return new CustomerAddressReponse
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId ?? string.Empty,
                Address = entity.Address ?? string.Empty,
                Success = success,
                Message = message
            };
        }

    }
}
