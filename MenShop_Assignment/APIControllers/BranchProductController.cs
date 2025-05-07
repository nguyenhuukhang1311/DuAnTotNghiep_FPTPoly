using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models;
using MenShop_Assignment.Models.BranchModel;
using MenShop_Assignment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchProductController : ControllerBase
    {
        private readonly BranchProductRepository _branchProductRepository;
        public BranchProductController(BranchProductRepository branchProductRepository)
        {
            _branchProductRepository = branchProductRepository;
        }

        [HttpGet("{branchId}")]
        public async Task<List<BranchProductViewModel>> GetProducts(int branchId)
        {
            return await _branchProductRepository.GetBranchProductsAsync(branchId);
        }

        [HttpGet("{branchId}/product/{productId}")]
        public async Task<List<BranchProductDetailViewModel>> GetProductDetail(int branchId, int productId)
        {
            return await _branchProductRepository.GetBranchProductDetailAsync(branchId, productId);
            
        }
    }
}
