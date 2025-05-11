using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Repositories
{
    public class OutputReceiptRespository
    {
        private readonly ApplicationDbContext _context;
        private readonly InputReceiptMapper _mapper;
        public OutputReceiptRespository(ApplicationDbContext context, InputReceiptMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task ConfirmOutputReceiptAsync(int receiptId)
        {
            // 1. Tìm OutputReceipt
            var outputReceipt = await _context.OutputReceipts.Where(x=>x.ReceiptId == receiptId).Include(x=>x.OutputReceiptDetails).FirstOrDefaultAsync();
            if (outputReceipt == null) throw new Exception("OutputReceipt không tồn tại");

            // 2. Cập nhật trạng thái
            outputReceipt.ConfirmedDate = DateTime.Now;
            outputReceipt.Status = OrderStatus.Completed;

            // 3. Duyệt danh sách cập nhật từng sản phẩm
            foreach (var detail in outputReceipt.OutputReceiptDetails)
            {
                var historyupdate = new HistoryPrice
                {
                    ProductDetailId = detail.ProductDetailId,
                    SellPrice = detail.Price,
                    UpdatedDate = DateTime.Now,
                };
                _context.HistoryPrices.Add(historyupdate);

                var branchDetail = _context.BranchDetails.Where(x=>x.BranchId == outputReceipt.BranchId && x.ProductDetailId == detail.ProductDetailId).FirstOrDefault();
                if (branchDetail == null)
                {
                    var newBranchDetail = new BranchDetail
                    {
                        BranchId = outputReceipt.BranchId,
                        ProductDetailId = detail.ProductDetailId,
                        Price = detail.Price,
                        Quantity = detail.Quantity,
                    };
                    _context.BranchDetails.Add(newBranchDetail);
                    continue;
                }
                branchDetail.Price = detail.Price;
                branchDetail.Quantity += detail.Quantity;
            }
            await _context.SaveChangesAsync();
        }
    }
}
