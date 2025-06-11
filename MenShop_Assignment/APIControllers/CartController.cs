using MenShop_Assignment.Repositories.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MenShop_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(customerId))
                return Unauthorized();

            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId);
            return Ok(cart);
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int productDetailId, int quantity)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(customerId))
                return Unauthorized();

            try
            {
                await _cartRepository.AddToCartAsync(customerId, productDetailId, quantity);
                return Ok(new { message = "Đã thêm vào giỏ hàng" });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    inner = ex.InnerException?.Message,
                    inner2 = ex.InnerException?.InnerException?.Message
                });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateQuantity(int productDetailId, int quantity)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(customerId))
                return Unauthorized();

            try
            {
                await _cartRepository.UpdateQuantityAsync(customerId, productDetailId, quantity);
                return Ok(new { message = "Cập nhật số lượng thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart(int productDetailId)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(customerId))
                return Unauthorized();

            try
            {
                await _cartRepository.RemoveFromCartAsync(customerId, productDetailId);
                return Ok(new { message = "Đã xoá sản phẩm khỏi giỏ hàng" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
