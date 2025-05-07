using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.OutputReceiptView;

namespace MenShop_Assignment.Mapper
{
    public class OutputReceiptMapper
    {
        private readonly ApplicationDbContext _context;
        public OutputReceiptMapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public OutputReceiptViewModel ToOutReceiptView(OutputReceipt outputReceipt)
        {
            return new OutputReceiptViewModel
            {
               ReceiptId = outputReceipt.ReceiptId,
               CreatedDate = outputReceipt.CreatedDate,
               CancelDate = outputReceipt.CancelDate,
               ConfirmedDate = outputReceipt.ConfirmedDate,
               Status = outputReceipt.Status.ToString(),
               Total = outputReceipt.Total,
               OutputReceiptDetails = outputReceipt.OutputReceiptDetails.Select(x=>ToReceiptDetailView(x)).ToList(),

            };
        }

        public ReceiptDetailViewModel ToReceiptDetailView(OutputReceiptDetail receiptDetail)
        {
            return new ReceiptDetailViewModel
            {
                Name = _context.Products.Where(x => x.ProductId == receiptDetail.ProductDetail.ProductId).FirstOrDefault().ProductName,
                Color = _context.Colors.Where(x=>x.ColorId == receiptDetail.ProductDetail.ColorId).FirstOrDefault().Name,
                Size = _context.Sizes.Where(x=>x.SizeId==receiptDetail.ProductDetail.SizeId).FirstOrDefault().Name,
                Fabric = _context.Fabrics.Where(x=>x.FabricId == receiptDetail.ProductDetail.FabricId).FirstOrDefault().Name,
                Quantity = receiptDetail.Quantity,
                Price = receiptDetail.Price,

            };
        }

    }
}
