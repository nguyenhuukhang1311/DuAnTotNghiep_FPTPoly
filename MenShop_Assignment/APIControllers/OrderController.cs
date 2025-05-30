using System.Security.Claims;
using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models.OrderModel;
using MenShop_Assignment.Repositories;
using MenShop_Assignment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IAutoOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        private readonly OrderMapper _orderMapper;
        private readonly IOrderCustomerRepository _orderCustomerRepository;


        public OrderController(IAutoOrderService orderService, ILogger<OrderController> logger, OrderMapper orderMapper, IOrderCustomerRepository orderCustomerRepository)
        {
            _orderService = orderService;
            _logger = logger;
            _orderMapper = orderMapper;
            _orderCustomerRepository = orderCustomerRepository;
        }

        [HttpPut("auto-approve")]
        public async Task<IActionResult> AutoApproveAll()
        {
            var result = await _orderService.ApproveAllOrdersAsync();
            return Ok(result);
        }

        [HttpPut("auto-approve/{orderId}")]
        public async Task<IActionResult> AutoApprove(int orderId)
        {
            var result = await _orderService.ApproveOrderAsync(orderId);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var result = await _orderService.GetPendingOrdersAsync();
            return Ok(result);
        }

        //Order for Customer 

        [HttpGet("getorder")]
        public async Task<ActionResult<ApiResponse<List<OrderCustomerModel>>>> GetOrdersByCustomerId(string? customerId)
        {
            try
            {
                var orders = await _orderCustomerRepository.GetOrdersByCustomerIdAsync(customerId);
                var orderDTOs = _orderMapper.MapToDTO(orders);

                return Ok(new ApiResponse<List<OrderCustomerModel>>(orderDTOs, $"Tìm thấy {orderDTOs.Count} đơn hàng"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<List<OrderCustomerModel>>(null, $"Lỗi: {ex.Message}"));
            }
        }

        [HttpGet("getorderdetail/{orderId}")]
        public async Task<ActionResult<ApiResponse<List<OrderDetailCustomerModel>>>> GetOrderById(int orderId)
        {
            try
            {
                if (orderId <= 0)
                    return BadRequest(new ApiResponse<List<OrderDetailCustomerModel>>(null, "Mã đơn hàng không hợp lệ"));

                var order = await _orderCustomerRepository.GetOrderWithDetailsAsync(orderId);
                if (order == null)
                    return NotFound(new ApiResponse<List<OrderDetailCustomerModel>>(null, "Không tìm thấy đơn hàng"));

                var orderDetails = order.Details?.Select(detail => _orderMapper.MapOrderDetailToDTO(detail)).ToList();
                return Ok(new ApiResponse<List<OrderDetailCustomerModel>>(orderDetails, "Lấy chi tiết đơn hàng thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<List<OrderDetailCustomerModel>>(null, $"Lỗi: {ex.Message}"));
            }
        }


        [HttpPost("search")]
        public async Task<ActionResult<ApiResponse<List<OrderCustomerModel>>>> SearchOrders([FromBody] OrderSearchCustomerModel searchDto)
        {
            try
            {
                if (searchDto == null)
                    return BadRequest(new ApiResponse<List<OrderCustomerModel>>(null, "Dữ liệu tìm kiếm không hợp lệ"));

                var orders = await _orderCustomerRepository.SearchOrdersAsync(searchDto);
                var orderDTOs = _orderMapper.MapToDTO(orders);

                return Ok(new ApiResponse<List<OrderCustomerModel>>(orderDTOs, $"Tìm thấy {orderDTOs.Count} đơn hàng"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<List<OrderCustomerModel>>(null, $"Lỗi: {ex.Message}"));
            }
        }


        [HttpPut("cancel")]
        public async Task<ActionResult<ApiResponse<bool>>> CancelOrder([FromBody] CancelOrderCustomer cancelDto)
        {
            try
            {
                if (cancelDto == null || cancelDto.OrderId <= 0)
                    return BadRequest(new ApiResponse<bool>(false, "Mã đơn hàng không hợp lệ"));

                var canCancel = await _orderCustomerRepository.CanCancelOrderAsync(cancelDto.OrderId);
                if (!canCancel)
                    return BadRequest(new ApiResponse<bool>(false, "Chỉ có thể hủy đơn hàng ở trạng thái Pending hoặc Confirmed"));

                var result = await _orderCustomerRepository.CancelOrderAsync(cancelDto.OrderId);

                if (result)
                    return Ok(new ApiResponse<bool>(true, "Hủy đơn hàng thành công"));
                else
                    return BadRequest(new ApiResponse<bool>(false, "Không thể hủy đơn hàng"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<bool>(false, $"Lỗi: {ex.Message}"));
            }
        }
    }
}
