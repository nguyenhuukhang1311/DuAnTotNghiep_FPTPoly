using MenShop_Assignment.Datas;
using MenShop_Assignment.Models;

namespace MenShop_Assignment.Mapper
{
	public class InputReceiptMapper
	{
		private readonly ApplicationDbContext _context;
		public InputReceiptMapper(ApplicationDbContext context)
		{
			_context = context;
		}
		public InputReceiptViewModel ToInputReceiptView(InputReceipt inputReceipt)
		{
			return new InputReceiptViewModel
			{
				ReceiptId = inputReceipt.ReceiptId,
				CancelDate = inputReceipt.CancelDate,
				CreatedDate = inputReceipt.CreatedDate,
				ConfirmedDate = inputReceipt.ConfirmedDate,
				ManagerName = _context.Users.Where(x=>x.Id.Equals(inputReceipt.ManagerId)).FirstOrDefault().UserName,
				Status = inputReceipt.Status.ToString(),
				StorageId = inputReceipt.StorageId,
				Total = inputReceipt.Total,
				InputReceiptDetails = inputReceipt.InputReceiptDetails.Select(x=>ToInputDetailView(x)).ToList(),
				
			};
		}
		public InputReceiptDetailViewModel ToInputDetailView(InputReceiptDetail inputReceiptDetail)
		{
			return new InputReceiptDetailViewModel
			{
				Quantity = inputReceiptDetail.Quantity,
				Price = inputReceiptDetail.Price,
				Name = _context.Products.Where(x => x.ProductId == inputReceiptDetail.ProductDetail.ProductId).FirstOrDefault().ProductName,
				Color = _context.Colors.Where(x => x.ColorId == inputReceiptDetail.ProductDetail.ColorId).FirstOrDefault().Name,
				Size = _context.Sizes.Where(x => x.SizeId == inputReceiptDetail.ProductDetail.SizeId).FirstOrDefault().Name,
				Fabric = _context.Fabrics.Where(x => x.FabricId == inputReceiptDetail.ProductDetail.SizeId).FirstOrDefault().Name,
			};
		}
	}
}
