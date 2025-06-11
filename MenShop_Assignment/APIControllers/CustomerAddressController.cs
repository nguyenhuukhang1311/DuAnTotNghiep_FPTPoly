using Microsoft.AspNetCore.Mvc;
using MenShop_Assignment.Models.CustomerModels;
using MenShop_Assignment.Repositories.CustomerAddress;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MenShop_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly ICustomerAddressRepository _repository;

        public CustomerAddressController(ICustomerAddressRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public async Task<ActionResult<List<CustomerAddressModel>>> GetAll()
        {
            var addresses = await _repository.GetAllAsync();
            return Ok(addresses);
        }



        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<CustomerAddressModel>>> GetByCustomerId(string customerId)
        {
            var addresses = await _repository.GetByCustomerIdAsync(customerId);
            return Ok(addresses);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerAddressReponse>> Create(CreateCustomerAddressDto dto)
        {
            var result = await _repository.AddAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<CustomerAddressReponse>> Update(UpdateCustomerAddressDto dto)
        {
            var result = await _repository.UpdateAsync(dto);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
