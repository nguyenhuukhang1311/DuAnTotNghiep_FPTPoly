using MenShop_Assignment.Models;
using MenShop_Assignment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InputReceiptController : ControllerBase
	{
		private readonly InputReceiptRepository _receiptRepository;
		public InputReceiptController(InputReceiptRepository receiptRepository)
		{
			_receiptRepository = receiptRepository;
		}
		[HttpGet]
		public async Task<List<InputReceiptViewModel>> GetInputReceipts()
		{
			return await _receiptRepository.GetInputReceipts();
		}
        [HttpGet("{id}")]
        public async Task<InputReceiptViewModel> GetById(int Id)
		{
			return await _receiptRepository.GetById(Id);
		}

	}
}
