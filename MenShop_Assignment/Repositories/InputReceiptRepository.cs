using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Repositories
{
	public class InputReceiptRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly InputReceiptMapper _mapper;
		public InputReceiptRepository(ApplicationDbContext context,InputReceiptMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<List<InputReceiptViewModel>> GetInputReceipts()
		{
			return await _context.InputReceipts.Select(x=>_mapper.ToInputReceiptView(x)).ToListAsync(); 
		}
		public async Task<InputReceiptViewModel> GetById(int Id)
		{
			var receipt = await _context.InputReceipts.Where(x=>x.ReceiptId == Id).FirstOrDefaultAsync();
			return _mapper.ToInputReceiptView(receipt);
		}
		//foreach(var x in ..){  }
	}
}
