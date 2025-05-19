using MenShop_Assignment.DTOs;
using MenShop_Assignment.Models;
using MenShop_Assignment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutputReceiptsController : ControllerBase
    {
        private readonly OutputReceiptRepository _repository;
        public OutputReceiptsController(OutputReceiptRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("getall")]
        public async Task<List<OutputReceiptViewModel>> GetOutputReceipts()
        {
            var outputReceipts = await _repository.GetOutputReceiptViews();
            return outputReceipts;
        }
        [HttpGet("getbyid")]
        public async Task<OutputReceiptViewModel> GetById(int Id)
        {
            var outputReceipts = await _repository.GetById(Id);
            return outputReceipts;
        }
        [HttpGet("getbybranchid")]
        public async Task<List<OutputReceiptViewModel>> GetByBranchId(int branchId)
        {
            var outputReceipts = await _repository.GetByBranchId(branchId);
            return outputReceipts;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateReceipt([FromBody] List<CreateReceiptDetailDTO> detailDTOs, [FromQuery] int branchId, [FromQuery] string managerId)
        {
            string result = await _repository.CreateReceipt(branchId,managerId,detailDTOs);
            if (result.ToLower().Contains("Đã thêm".ToLower()))
            {
                return Ok(new { message = result });
            }
            else
            {
                return BadRequest(new {error = result});
            }
        }
        [HttpPut("confirm")]
        public async Task<IActionResult> ConfirmReceipt(int Id)
        {
            string result = await _repository.ConfirmReceipt(Id);

            if (result.ToLower().StartsWith("Đã xác nhận".ToLower()))
            {
                return Ok(new { message = result });
            }
            else
            {
                return BadRequest(new { error = result });
            }
        }
        [HttpPatch("cancel")]
        public async Task<IActionResult> CancelReceipt(int Id)
        {
            string result = await _repository.CancelReceipt(Id);
            if (result.ToLower().Contains("Đã hủy".ToLower()))
            {
                return Ok(new { message = result });
            }
            else
            {
                return BadRequest(new { error = result });
            }
        }
    }
}
