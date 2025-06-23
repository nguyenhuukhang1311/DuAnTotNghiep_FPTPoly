using MenShop_Assignment.DTOs;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models;
using MenShop_Assignment.Repositories.BranchesRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
		private readonly IBranchRepository _branchRepository;
		public BranchController(IBranchRepository branchRepository)
		{
			_branchRepository = branchRepository;
		}
		[HttpGet("getbranches")]
		public async Task<IActionResult> GetAllBranches()
		{
			var branches = await _branchRepository.GetBranchesAsync();
			if (branches == null)
				return NotFound(new ApiResponseModel<List<BranchViewModel>>(false, "Không tìm thấy chi nhánh", null, 404));
			return Ok(new ApiResponseModel<List<BranchViewModel>>(true, "Lấy danh sách chi nhánh thành công", branches, 200));
		}

		[HttpGet("getbranch/{id}")]
		public async Task<IActionResult> GetBranchById(int id)
		{
			var branch = await _branchRepository.GetBranchByIdAsync(id);
			if (branch == null)
				return NotFound(new ApiResponseModel<BranchViewModel>(false, "Chi nhánh không tồn tại", null, 404));
			return Ok(new ApiResponseModel<BranchViewModel>(true, "Lấy chi nhánh thành công", branch, 200));
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateBranch([FromBody] CreateUpdateBranchDTO dto)
		{
			var branch = await _branchRepository.CreateBranchAsync(dto);
			if (branch == null)
				return BadRequest(new ApiResponseModel<object>(false, "Tạo chi nhánh thất bại", null, 400));
			var viewModel = BranchMapper.ToBranchViewModel(branch);
			return Ok(new ApiResponseModel<BranchViewModel>(true, "Tạo chi nhánh thành công", viewModel, 201));
		}

		[HttpPut("update")]
		public async Task<IActionResult> UpdateBranch([FromBody] CreateUpdateBranchDTO dto)
		{
			var branch = await _branchRepository.UpdateBranchAsync(dto);
			if (branch == null)
				return NotFound(new ApiResponseModel<object>(false, "Không cập nhật được chi nhánh", null, 404));
			var viewModel = BranchMapper.ToBranchViewModel(branch);
			return Ok(new ApiResponseModel<BranchViewModel>(true, "Cập nhập chi nhánh thành công", viewModel, 201));
		}

		[HttpGet("{branchId}/products")]
		public async Task<IActionResult> GetBranchProducts(int branchId)
		{
			var products = await _branchRepository.GetBranchProductsAsync(branchId);
			if (products == null)
				return NotFound(new ApiResponseModel<object>(false, "Không tìm thấy sản phẩm tại chi nhánh", null, 404));
			return Ok(new ApiResponseModel<List<ProductViewModel>>(true, "Lấy sản phẩm thành công", products, 200));
		}
		[HttpGet("{branchId}/products/{productId}/details")]
		public async Task<IActionResult> GetProductDetailsInBranch(int branchId, int productId)
		{
			var details = await _branchRepository.GetDetailProductBranchAsync(branchId, productId);
			if (details == null)
				return NotFound(new ApiResponseModel<object>(false, "Không tìm thấy chi tiết sản phẩm", null, 404));
			return Ok(new ApiResponseModel<object>(true, "Lấy chi tiết sản phẩm thành công", details, 200));
		}
		[HttpGet("{branchId}/search")]
		public async Task<IActionResult> SearchProductInBranch(int branchId, [FromQuery] string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
				return BadRequest(new ApiResponseModel<object>(false, "Chuỗi tìm kiếm không hợp lệ", null, 400));
			bool isAlpha = searchTerm.All(char.IsLetter);
			bool isNumeric = searchTerm.All(char.IsDigit);
			if (!isAlpha && !isNumeric)
			{
				return BadRequest(new ApiResponseModel<object>(false, "Chuỗi tìm kiếm chỉ được chứa chữ hoặc số, không bao gồm ký tự đặc biệt hoặc trộn lẫn", null, 400));
			}
			List<ProductViewModel> results;
			if (isAlpha)
				results = await _branchRepository.SmartSearchProductsByNameAsync(branchId, searchTerm);
			else
				results = await _branchRepository.SmartSearchProductsByIdAsync(branchId, int.Parse(searchTerm));
			return Ok(new ApiResponseModel<object>(true, "Tìm kiếm thành công", results, 200));
		}
	}
}
