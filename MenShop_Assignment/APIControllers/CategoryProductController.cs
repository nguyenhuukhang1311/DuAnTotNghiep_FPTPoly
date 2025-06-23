using MenShop_Assignment.DTOs;
using MenShop_Assignment.Models;
using MenShop_Assignment.Repositories.Category;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryProductController : ControllerBase
	{
		private readonly ICategoryProductRepository _categoryRepo;

		public CategoryProductController(ICategoryProductRepository categoryRepo)
		{
			_categoryRepo = categoryRepo;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var categories = await _categoryRepo.GetAllCategoriesAsync();
			if (categories == null || !categories.Any())
				return NotFound(new ApiResponseModel<object>(false, "Không tìm thấy loại sản phẩm nào", null, 404));

			return Ok(new ApiResponseModel<List<CategoryProductViewModel>>(true, "Lấy danh sách loại sản phẩm thành công", categories, 200));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var category = await _categoryRepo.GetCategoryByIdAsync(id);
			if (category == null)
				return NotFound(new ApiResponseModel<object>(false, "Không tìm thấy loại sản phẩm", null, 404));

			return Ok(new ApiResponseModel<CategoryProductViewModel>(true, "Lấy loại sản phẩm thành công", category, 200));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateUpdateCategoryDTO dto)
		{
			var success = await _categoryRepo.CreateCategoryAsync(dto);
			if (!success)
				return BadRequest(new ApiResponseModel<object>(false, "Tạo loại sản phẩm thất bại", null, 400));

			return Ok(new ApiResponseModel<object>(true, "Tạo loại sản phẩm thành công", null, 201));
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] CreateUpdateCategoryDTO dto)
		{
			var success = await _categoryRepo.UpdateCategoryAsync(dto);
			if (!success)
				return NotFound(new ApiResponseModel<object>(false, "Không tìm thấy loại sản phẩm để cập nhật", null, 404));

			return Ok(new ApiResponseModel<object>(true, "Cập nhật loại sản phẩm thành công", null, 200));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var success = await _categoryRepo.RemoveCategoryAsync(id);
			if (!success)
				return NotFound(new ApiResponseModel<object>(false, "Không tìm thấy loại sản phẩm để xoá", null, 404));

			return Ok(new ApiResponseModel<object>(true, "Xoá loại sản phẩm thành công", null, 200));
		}
	}
}
