using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;
using MenShop_Assignment.Models;
using MenShop_Assignment.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository shipperRepository)
        {
            _orderRepository = shipperRepository;
        }

        [HttpGet("getallorders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderRepository.GetAllOrdersAsync();
            return Ok(result);
        }
        [HttpGet("getallonlineorders")]
        public async Task<IActionResult> GetAllOnlineOrders()
        {
            var onlineOrders = await _orderRepository.GetAllOnlineOrdersAsync();
            return Ok(onlineOrders);
        }
        [HttpPut("UpdateOrderShipperStatus")]
        public async Task<IActionResult> UpdateOrderShipperStatus(int orderId, string shipperId)
        {
            await _orderRepository.ShipperAcceptOrderByOrderId(orderId,shipperId);
            return NoContent();
        }
        [HttpPut("CompletedOrder")]
        public async Task<IActionResult> CompleteOrderStatus(int orderId)
        {
            await _orderRepository.CompletedOrderStatus(orderId);
            return NoContent();
        }
        [HttpGet("getorders")]
        public async Task<IActionResult> GetOrdersByShipperId(string shipperId)
        {
            var result = await _orderRepository.GetOrdersByShipperId(shipperId);
            return Ok(result);
        }
        [HttpGet("getordersbyaddress")]
        public async Task<IActionResult> GetOrdersByShipperAddress()
        {
            return NoContent();
        }
        [HttpGet("getordersbydistrict")]
        public async Task<ActionResult<List<OrdersViewModel>>> GetOrdersByDistrict(string district)
        {
            var orders = await _orderRepository.GetOrdersByDistrict(district);
            return Ok(orders);
        }
    }
}
