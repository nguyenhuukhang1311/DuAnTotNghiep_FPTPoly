using MenShop_Assignment.Models.OutputReceiptView;
using MenShop_Assignment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutputReceiptController : ControllerBase
    {
        private readonly OutputReceiptViewRepository _receiptRepository;

        public OutputReceiptController(OutputReceiptViewRepository viewRepository)
        {
            _receiptRepository = viewRepository;
        }

        // GET: api/OutputReceipt
        [HttpGet]
        public async Task<List<OutputReceiptViewModel>> GetAllOutPutReceipts()
        {
            return await _receiptRepository.GetOutputReceipts();
        }

        // GET: api/OutputReceipt/5
        [HttpGet("{id}")]
        public async Task<OutputReceiptViewModel> GetById(int id)
        {
            return await _receiptRepository.GetById(id);
        }
    }

}
