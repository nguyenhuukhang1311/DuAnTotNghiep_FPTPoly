using MenShop_Assignment.Datas;
using MenShop_Assignment.DTOs;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Repositories
{
    public class OutputReceiptRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ReceiptMapper _mapper;
        public OutputReceiptRepository(ApplicationDbContext context, ReceiptMapper mapper) 
        {
            _context = context; 
            _mapper = mapper;
        }
        public async Task<List<OutputReceiptViewModel>> GetOutputReceiptViews()
        {
            var outputReceipts = await _context.OutputReceipts.ToListAsync();
            var results = new List<OutputReceiptViewModel>();
            foreach (var receipt in outputReceipts)
            {
                results.Add(await _mapper.ToOutputReceiptView(receipt));
            }
            return results;
        }
        public async Task<OutputReceiptViewModel> GetById(int Id)
        {
            var receipt = await _context.OutputReceipts.Where(x => x.ReceiptId == Id).FirstOrDefaultAsync();
            return await _mapper.ToOutputReceiptView(receipt);
        }
        public async Task<List<OutputReceiptViewModel>> GetByBranchId(int BranchId)
        {
            var outputReceipts = await _context.OutputReceipts.Where(x => x.BranchId == BranchId).ToListAsync();
            var results = new List<OutputReceiptViewModel>();
            foreach (var receipt in outputReceipts)
            {
                results.Add(await _mapper.ToOutputReceiptView(receipt));
            }
            return results;
        }
        public async Task<string> ConfirmReceipt(int Id)
        {
            try
            {
                var outputReceipt = _context.OutputReceipts.Where(x => x.ReceiptId == Id).Include(c => c.OutputReceiptDetails).FirstOrDefault();
                if (outputReceipt != null)
                {
                    outputReceipt.ConfirmedDate = DateTime.UtcNow;
                    outputReceipt.Status = Extensions.OrderStatus.Completed;
                    if (outputReceipt.OutputReceiptDetails != null)
                    {
                        foreach (var detail in outputReceipt.OutputReceiptDetails)
                        {
                            var historyPrice = new HistoryPrice
                            {
                                SellPrice = detail.Price,
                                ProductDetailId = detail.ProductDetailId,
                                UpdatedDate = DateTime.UtcNow
                            };
                            _context.HistoryPrices.Add(historyPrice);
                        }
                    }
                    foreach (var detail in outputReceipt.OutputReceiptDetails)
                    {
                        var branchDetail = _context.BranchDetails.Where(x => x.BranchId == outputReceipt.BranchId && x.ProductDetailId == detail.ProductDetailId).FirstOrDefault();
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
                return "Đã xác nhận phiếu xuất thành công";
            }
            catch (Exception ex)
            {
                return "Xảy ra lỗi: " + ex.Message;
            }
        }
        public async Task<string> CreateReceipt(int branchId, string managerId, List<CreateReceiptDetailDTO> detailDTOs)
        {
            try
            {
                var outputDetails = detailDTOs.Select(x => _mapper.ToOutputReceiptDetail(x)).ToList();
                var newOutputReceipt = new OutputReceipt
                {
                    BranchId = branchId,
                    CreatedDate = DateTime.UtcNow,
                    ManagerId = managerId,
                    Status = Extensions.OrderStatus.Created,
                    Total = 0,
                };
                await _context.OutputReceipts.AddAsync(newOutputReceipt);
                await _context.SaveChangesAsync();
                decimal total = 0;
                foreach (var detail in outputDetails)
                {
                    detail.ReceiptId = newOutputReceipt.ReceiptId;
                    detail.OutputReceipt = newOutputReceipt;
                    total += (decimal)detail.Price;
                    StorageDetail storageDetail = _context.StorageDetails.Where(x=>x.ProductDetailId==detail.ProductDetailId).FirstOrDefault();
                    storageDetail.Quantity -= detail.Quantity;
                    await _context.OutputReceiptDetails.AddAsync(detail);
                }
                newOutputReceipt.Total = total;
                await _context.SaveChangesAsync();
                return "Đã thêm thành công";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
        public async Task<string> CancelReceipt(int Id)
        {
            try
            {
                var receipt = await _context.OutputReceipts.Where(x => x.ReceiptId == Id).FirstOrDefaultAsync();
                if (receipt.Status == Extensions.OrderStatus.Pending)
                {
                    receipt.Status = Extensions.OrderStatus.Cancelled;
                    await _context.SaveChangesAsync();
                }
                return "Đã hủy phiếu thành công";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }

        }
    }
}
