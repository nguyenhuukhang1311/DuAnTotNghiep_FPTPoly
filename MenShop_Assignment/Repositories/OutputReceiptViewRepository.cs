using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models.OutputReceiptView;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Repositories
{
    public class OutputReceiptViewRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly OutputReceiptMapper _mapper;
        public OutputReceiptViewRepository(ApplicationDbContext context, OutputReceiptMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<OutputReceiptViewModel>> GetOutputReceipts()
        {
            return await _context.OutputReceipts.Select(x => _mapper.ToOutReceiptView(x)).ToListAsync();
        }
        public async Task<OutputReceiptViewModel> GetById(int Id)
        {
            var receipt = await _context.OutputReceipts.Where(x=>x.ReceiptId == Id).FirstOrDefaultAsync();
            return _mapper.ToOutReceiptView(receipt);
        }
    }
}
