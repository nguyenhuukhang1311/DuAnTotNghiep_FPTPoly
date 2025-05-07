using MenShop_Assignment.Repositories.Category;
using MenShop_Assignment.Models.CategoryModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MenShop_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryProductRepository _categoryProductRepository;

        public CategoryController(ICategoryProductRepository categoryProductRepository)
        {
            _categoryProductRepository = categoryProductRepository;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModelView>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryProductRepository.GetAllCategoriesAsync();
                if (categories == null || categories.Count() == 0)
                {
                    return NotFound("Không tìm thấy danh mục nào.");
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi khi lấy danh mục: {ex.Message}");
            }
        }

        // GET: api/category/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModelView>> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryProductRepository.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound($"Không tìm thấy danh mục với ID {id}.");
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi khi lấy danh mục với ID {id}: {ex.Message}");
            }
        }

        // POST: api/category
        [HttpPost]
        public async Task<ActionResult<CategoryModelView>> CreateCategory(CategoryModelView category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Dữ liệu danh mục không hợp lệ. Vui lòng cung cấp đầy đủ thông tin yêu cầu.");
                }

                var createdCategory = await _categoryProductRepository.CreateCategoryAsync(category);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.CategoryId }, createdCategory);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message;
                return StatusCode(500, $"Đã xảy ra lỗi khi tạo danh mục: {ex.Message} | Chi tiết: {innerMessage}");
            }

        }

        // PUT: api/category/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryModelView>> UpdateCategory(int id, CategoryModelView category)
        {
            try
            {
                if (id != category.CategoryId)
                {
                    return BadRequest("ID danh mục không hợp lệ!");
                }

                var updatedCategory = await _categoryProductRepository.UpdateCategoryAsync(category);
                if (updatedCategory == null)
                {
                    return NotFound($"Không tìm thấy danh mục với ID {id}.");
                }

                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi khi cập nhật danh mục: {ex.Message}");
            }
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveCategory(int id)
        {
            try
            {
                var isRemoved = await _categoryProductRepository.RemoveCategoryAsync(id);
                if (!isRemoved)
                {
                    return NotFound($"Không tìm thấy danh mục với ID {id} hoặc không thể xóa vì danh mục có sản phẩm!");
                }

                return Ok($"Danh mục đã được xóa thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi khi xóa danh mục: {ex.Message}");
            }
        }

    }
}

