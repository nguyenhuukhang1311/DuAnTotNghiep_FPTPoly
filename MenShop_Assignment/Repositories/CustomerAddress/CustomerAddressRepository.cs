using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models.CustomerModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MenShop_Assignment.Repositories.CustomerAddress;

namespace MenShop_Assignment.Repositories
{
    public class CustomerAddressRepository : ICustomerAddressRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly MapperCustomerAddress _mapper;

        public CustomerAddressRepository(ApplicationDbContext context, MapperCustomerAddress mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CustomerAddressModel>> GetAllAsync()
        {
            var entities = await _context.CustomerAddresses.ToListAsync();
            return entities.Select(e => _mapper.MapToCustomerAddressDto(e)).ToList();
        }


        public async Task<List<CustomerAddressModel>> GetByCustomerIdAsync(string customerId)
        {
            var entities = await _context.CustomerAddresses
                .Where(ca => ca.CustomerId == customerId)
                .ToListAsync();

            return entities.Select(e => _mapper.MapToCustomerAddressDto(e)).ToList();
        }

        public async Task<CustomerAddressReponse> AddAsync(CreateCustomerAddressDto dto)
        {
            try
            {
                var idUser = await _context.CustomerAddresses.FirstOrDefaultAsync(cd => cd.CustomerId == dto.CustomerId);
                if (idUser == null)
                {
                    return new CustomerAddressReponse
                    {
                        Success = false,
                        Message = "Khách hàng không tồn tại"
                    };
                }
                var entity = _mapper.MapToCustomerAddress(dto);
                _context.CustomerAddresses.Add(entity);
                await _context.SaveChangesAsync();

                return _mapper.MapToCustomerAddressResponse(entity, true, "Thêm địa chỉ khách hàng thành công");
            }
            catch (Exception ex)
            {

                return new CustomerAddressReponse
                {
                    Success = false,
                    Message = "Đã xảy ra lỗi khi thêm địa chỉ khách hàng: " + ex.Message,
                };
            }
        }


        public async Task<CustomerAddressReponse> UpdateAsync(UpdateCustomerAddressDto dto)
        {
            var entity = await _context.CustomerAddresses.FirstOrDefaultAsync(cd => cd.Id == dto.Id);
            if (entity == null)
            {
                return new CustomerAddressReponse
                {
                    Success = false,
                    Message = "Không tìm thấy địa chỉ khách hàng để cập nhật"
                };
            }

            entity.Address = dto.Address;
            // Nếu cần cập nhật CustomerId thì thêm ở đây
            await _context.SaveChangesAsync();
            return _mapper.MapToCustomerAddressResponse(entity, true, "Cập nhật địa chỉ khách hàng thành công");
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.CustomerAddresses.FirstOrDefaultAsync(cd => cd.Id == id);
            if (entity != null)
            {
                _context.CustomerAddresses.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
