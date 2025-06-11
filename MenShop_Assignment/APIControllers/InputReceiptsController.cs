using MenShop_Assignment.Datas;
using MenShop_Assignment.DTOs;
using MenShop_Assignment.Models;
using MenShop_Assignment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InputReceiptsController : ControllerBase
	{
		private readonly InputReceiptRepository _receiptRepository;
		public InputReceiptsController(InputReceiptRepository receiptRepository)
		{
			_receiptRepository = receiptRepository;
		}
		[HttpGet("getall")]
		public async Task<List<InputReceiptViewModel>> GetInputReceipts()
		{
			return await _receiptRepository.GetInputReceipts();
		}
		[HttpGet("getbyid")]
		public async Task<InputReceiptViewModel> GetById(int Id)
		{
			return await _receiptRepository.GetById(Id);
		}
		[HttpPut("confirm")]
		public async Task<IActionResult> ConfirmReceipt(int Id)
		{
			string result = await _receiptRepository.ConfirmReceipt(Id);

			if (result.ToLower().StartsWith("Đã xác nhận".ToLower()))
			{
				return Ok(new { message = result });
			}
			else
			{
				return BadRequest(new { error = result });
			}
		}
		[HttpPost("create")]
		public async Task<IActionResult> CreateReceipt([FromBody] List<CreateReceiptDetailDTO> detailDTOs, [FromQuery] string ManagerId)
		{
			string result = await _receiptRepository.CreateInputReceipt(detailDTOs, ManagerId);
			if (result.ToLower().Contains("Đã thêm".ToLower()))
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
			string result = await _receiptRepository.CancelReceipt(Id);
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
