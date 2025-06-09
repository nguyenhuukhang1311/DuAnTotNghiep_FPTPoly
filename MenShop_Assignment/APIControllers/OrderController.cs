using MenShop_Assignment.Models.OrderModels.CreateOrder;
using MenShop_Assignment.Models.OrderModels.OrderReponse;
using MenShop_Assignment.Models.Payment;

using MenShop_Assignment.Models.ProductModels.CreateProduct;
using MenShop_Assignment.Models.ProductModels.ReponseDTO;
using MenShop_Assignment.Repositories.OrderRepositories;
using MenShop_Assignment.Repositories.Product;
using MenShop_Assignment.Services.PaymentServices;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost("createOrder")]
        public async Task<ActionResult<OrderResponseDto>> CreateProductAsync([FromBody] CreateOrderDto createOrderDto)
        {
            if (createOrderDto == null)
            {
                return BadRequest("Dữ liệu không hợp lệ. Vui lòng cung cấp đầy đủ thông tin yêu cầu.");
            }

            try
            {
                var orderResponse = await _orderRepository.CreateOrderAsync(createOrderDto);
                return Ok(orderResponse);

            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Có lỗi khi tạo hóa đơn: {ex.Message}");
            }
        }

    }
}
