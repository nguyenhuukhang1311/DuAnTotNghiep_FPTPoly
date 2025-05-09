using MenShop_Assignment.Datas;
using MenShop_Assignment.Models;
using MenShop_Assignment.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageRepository _storageRepository;
        public StorageController(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStorages()
        {
            var result = await _storageRepository.GetAllStoragesAsync();
            return Ok(result);
        }
        [HttpGet("getdetail")]
        public async Task<IActionResult> GetDetailByProductId(int productId)
        {
            var result = await _storageRepository.GetDetailsByProductId(productId);
            return Ok(result);
        }
        [HttpGet("getproducts")]
        public async Task<IActionResult> GetProductByStorageId(int storageId)
        {
            var result = await _storageRepository.GetProductsByStorageIdAsync(storageId);
            return Ok(result);
        }
    }
}
