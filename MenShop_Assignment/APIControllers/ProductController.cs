using MenShop_Assignment.Models.ProductModels.CreateProduct;
using MenShop_Assignment.Models.ProductModels.ReponseDTO;
using MenShop_Assignment.Models.ProductModels.UpdateProduct;
using MenShop_Assignment.Models.ProductModels.ViewModel;
using MenShop_Assignment.Repositories.Product;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(new { message = $"Không tìm thấy sản phẩm với ID = {id}" });
            }
            return Ok(product);
        }

        [HttpGet("Category/{id}")]
        public async Task<ActionResult<ProductViewModel>> GetProductByCategoryID(int id)
        {
            var product = await _productRepository.GetProductByCategoryAsync(id);
            if (product == null)
            {
                return NotFound(new { message = $"Không tìm thấy sản phẩm với Mã danh mục = {id}" });
            }
            return Ok(product);
        }

        // POST api/products
        [HttpPost]
        public async Task<ActionResult<ProductResponseDTO>> CreateProductAsync([FromBody] CreateProductDTO createProductDTO)
        {
            if (createProductDTO == null)
            {
                return BadRequest("Dữ liệu không hợp lệ. Vui lòng cung cấp đầy đủ thông tin yêu cầu.");
            }

            try
            {
                var productResponse = await _productRepository.CreateProductAsync(createProductDTO);
                return CreatedAtAction(nameof(GetProductById), new { id = productResponse.ProductId }, productResponse);
            }
            catch (Exception ex)
            {
 
                return StatusCode(500, $"Có lỗi khi tạo sản phẩm: {ex.Message}");
            }
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductDTO updateProductDTO)
        {
            try
            {
                var updatedProduct = await _productRepository.UpdateProductAsync(productId, updateProductDTO);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("details/{detailId}")]
        public async Task<IActionResult> DeleteProductDetail(int detailId)
        {
            try
            {
                await _productRepository.DeleteProductDetailAsync(detailId);
                return Ok("Xóa chi tiết sản phẩm thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                await _productRepository.DeleteProductAsync(productId);
                return Ok("Xóa sản phẩm thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    details = ex.StackTrace
                });
            }
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductOnlyDTO dto)
        {
            var result = await _productRepository.CreateProductOnlyAsync(dto);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        // 2. Thêm chi tiết sản phẩm
        [HttpPost("add-detail")]
        public async Task<IActionResult> AddProductDetail([FromBody] AddProductDetailDTO dto)
        {
            var result = await _productRepository.AddProductDetailsAsync(dto);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        // 3. Thêm ảnh cho chi tiết sản phẩm
        [HttpPost("add-images/{detailId}")]
        public async Task<IActionResult> AddImages(int detailId, [FromBody] List<string> imageUrls)
        {
            var result = await _productRepository.AddImagesToDetailAsync(detailId, imageUrls);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
