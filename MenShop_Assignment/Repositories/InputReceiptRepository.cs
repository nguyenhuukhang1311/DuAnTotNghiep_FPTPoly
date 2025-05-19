using MenShop_Assignment.Datas;
using MenShop_Assignment.DTOs;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Repositories
{
    public class InputReceiptRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ReceiptMapper _mapper;
        public InputReceiptRepository(ApplicationDbContext context, ReceiptMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<InputReceiptViewModel>> GetInputReceipts()
        {
            // Lấy danh sách InputReceipts trước
            var inputReceipts = await _context.InputReceipts.ToListAsync();

            // Sau đó chuyển đổi từng phần tử và chờ các task hoàn thành
            var results = new List<InputReceiptViewModel>();
            foreach (var receipt in inputReceipts)
            {
                results.Add(await _mapper.ToInputReceiptView(receipt));
            }

            return results;
        }
        public async Task<InputReceiptViewModel> GetById(int Id)
        {
            var receipt = await _context.InputReceipts.Where(x => x.ReceiptId == Id).FirstOrDefaultAsync();
            return await _mapper.ToInputReceiptView(receipt);
        }
        public async Task<string> ConfirmReceipt(int Id)
        {
            try
            {
                var inputReceipt = _context.InputReceipts.Where(x => x.ReceiptId == Id).Include(c => c.InputReceiptDetails).FirstOrDefault();
                if (inputReceipt != null)
                {
                    inputReceipt.ConfirmedDate = DateTime.UtcNow;
                    inputReceipt.Status = Extensions.OrderStatus.Completed;
                    if (inputReceipt.InputReceiptDetails != null)
                    {
                        foreach (var detail in inputReceipt.InputReceiptDetails)
                        {
                            var historyPrice = new HistoryPrice
                            {
                                InputPrice = detail.Price,
                                ProductDetailId = detail.ProductDetailId,
                                UpdatedDate = DateTime.UtcNow
                            };
                            _context.HistoryPrices.Add(historyPrice);
                        }
                    }
                    foreach (var detail in inputReceipt.InputReceiptDetails)
                    {
                        var storageDetail = _context.StorageDetails.Where(x => x.StorageId == inputReceipt.StorageId && x.ProductDetailId == detail.ProductDetailId).FirstOrDefault();
                        if (storageDetail == null)
                        {
                            var newStorageDetail = new StorageDetail
                            {
                                StorageId = inputReceipt.StorageId,
                                ProductDetailId = detail.ProductDetailId,
                                Price = detail.Price,
                                Quantity = detail.Quantity,
                            };
                            await _context.StorageDetails.AddAsync(newStorageDetail);
                            continue;
                        }
                        storageDetail.Price = detail.Price;
                        storageDetail.Quantity += detail.Quantity;
                    }
                    _context.SaveChanges();
                }
                return "Đã xác nhận phiếu nhập thành công";
            }
            catch (Exception ex)
            {
                return "Xảy ra lỗi: " + ex.Message;
            }
        }
        public async Task<string> CreateInputReceipt(List<CreateReceiptDetailDTO> detailDTOs, string ManagerId)
        {
            try
            {
                List<InputReceiptDetail> inputReceiptDetails = detailDTOs.Select(x => _mapper.ToInputReceiptDetail(x)).ToList();
                var productDetailIds = inputReceiptDetails.Select(ird => ird.ProductDetailId).ToList();
                var uniqueCategoryId = _context.ProductDetails.Where(pd => productDetailIds.Contains(pd.DetailId))
                    .Include(pd => pd.Product)
                    .Select(pd => pd.Product.CategoryId.Value)
                    .Distinct()
                    .ToList();
                for (int i = 0; i < uniqueCategoryId.Count; i++)
                {
                    var newReceipt = new InputReceipt
                    {
                        ConfirmedDate = DateTime.Now,
                        ManagerId = ManagerId,
                        Status = Extensions.OrderStatus.Created,
                        StorageId = _context.Storages.Where(x => x.CategoryId == uniqueCategoryId[i]).FirstOrDefault().StorageId,
                        Total = 0,
                    };
                    _context.InputReceipts.Add(newReceipt);
                    await _context.SaveChangesAsync();
                    decimal total = 0;
                    foreach (var detail in inputReceiptDetails)
                    {
                        total += detail.Price.Value;
                        var productDetail = await _context.ProductDetails.Where(x => x.DetailId == detail.ProductDetailId).Include(x => x.Product).FirstOrDefaultAsync();
                        if (productDetail.Product.CategoryId == uniqueCategoryId[i])
                        {
                            var detailCopy = new InputReceiptDetail
                            {
                                ReceiptId = newReceipt.ReceiptId,
                                ProductDetailId = detail.ProductDetailId,
                                Quantity = detail.Quantity,
                                Price = detail.Price
                            };
                            _context.InputReceiptDetails.Add(detailCopy);
                        }
                    }
                    newReceipt.Total = total;
                    await _context.SaveChangesAsync();
                }
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
                var receipt = await _context.InputReceipts.Where(x => x.ReceiptId == Id).FirstOrDefaultAsync();
                if (receipt.Status != Extensions.OrderStatus.Pending)
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
