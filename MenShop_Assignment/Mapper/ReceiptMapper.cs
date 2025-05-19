using MenShop_Assignment.Datas;
using MenShop_Assignment.DTOs;
using MenShop_Assignment.Models;

namespace MenShop_Assignment.Mapper
{
	public class ReceiptMapper
	{
		private readonly ApplicationDbContext _context;
		private readonly UserMapper _userMapper;
		public ReceiptMapper(ApplicationDbContext context,UserMapper userMapper)
		{
			_context = context;
			_userMapper = userMapper;
		}

        public Task<OutputReceiptViewModel> ToOutputReceiptView(OutputReceipt outputReceipt)
        {
			return Task.FromResult(new OutputReceiptViewModel
			{
				ReceiptId = outputReceipt.ReceiptId,
				CancelDate = outputReceipt.CancelDate,
				ConfirmedDate = outputReceipt.ConfirmedDate,
				CreatedDate = outputReceipt.CreatedDate,
				ManagerName = _context.Users.Where(x => x.Id.Equals(outputReceipt.ManagerId)).FirstOrDefault().UserName,
				BranchName = "Chi nhánh " + _context.Branches.Where(x => x.BranchId == outputReceipt.BranchId).FirstOrDefault().Address,
				Status = outputReceipt.Status.ToString(),
				Total = outputReceipt.Total,
                OutputReceiptDetails = outputReceipt.OutputReceiptDetails.Select(x => ToDetailView(x)).ToList(),
			});
        }
        public Task<InputReceiptViewModel> ToInputReceiptView(InputReceipt inputReceipt)
		{
			return Task.FromResult(new InputReceiptViewModel
			{
				ReceiptId = inputReceipt.ReceiptId,
				CancelDate = inputReceipt.CancelDate,
				CreatedDate = inputReceipt.CreatedDate,
				ConfirmedDate = inputReceipt.ConfirmedDate,
				ManagerName = _context.Users.Where(x=>x.Id.Equals(inputReceipt.ManagerId)).FirstOrDefault().UserName,
				Status = inputReceipt.Status.ToString(),
				StorageName = "Kho "+ _context.CategoryProducts.Where(x=>x.CategoryId==inputReceipt.Storage.CategoryId).FirstOrDefault().Name,
				Total = inputReceipt.Total,
				InputReceiptDetails = inputReceipt.InputReceiptDetails.Select(x=>ToDetailView(x)).ToList(),
			});
		}
		public ReceiptDetailViewModel ToDetailView(InputReceiptDetail inputReceiptDetail)
		{
			return new ReceiptDetailViewModel
			{
				Quantity = inputReceiptDetail.Quantity,
				Price = inputReceiptDetail.Price,
				Name = _context.Products.Where(x => x.ProductId == inputReceiptDetail.ProductDetail.ProductId).FirstOrDefault().ProductName,
				Color = _context.Colors.Where(x => x.ColorId == inputReceiptDetail.ProductDetail.ColorId).FirstOrDefault().Name,
				Size = _context.Sizes.Where(x => x.SizeId == inputReceiptDetail.ProductDetail.SizeId).FirstOrDefault().Name,
				Fabric = _context.Fabrics.Where(x => x.FabricId == inputReceiptDetail.ProductDetail.SizeId).FirstOrDefault().Name,
				ImageFullPath = _context.ImagesProducts.Where(x=>x.ProductDetailId==inputReceiptDetail.ProductDetailId).FirstOrDefault().FullPath,
			};
		}
		public ReceiptDetailViewModel ToDetailView(OutputReceiptDetail outputReceiptDetail)
		{
			return new ReceiptDetailViewModel
			{
				Quantity = outputReceiptDetail.Quantity,
				Price = outputReceiptDetail.Price,
				Name = _context.Products.Where(x => x.ProductId == outputReceiptDetail.ProductDetail.ProductId).FirstOrDefault().ProductName,
				Color = _context.Colors.Where(x => x.ColorId == outputReceiptDetail.ProductDetail.ColorId).FirstOrDefault().Name,
				Size = _context.Sizes.Where(x => x.SizeId == outputReceiptDetail.ProductDetail.SizeId).FirstOrDefault().Name,
				Fabric = _context.Fabrics.Where(x => x.FabricId == outputReceiptDetail.ProductDetail.SizeId).FirstOrDefault().Name,
				ImageFullPath = _context.ImagesProducts.Where(x => x.ProductDetailId == outputReceiptDetail.ProductDetailId).FirstOrDefault().FullPath,
			};
		}
		public InputReceiptDetail ToInputReceiptDetail(CreateReceiptDetailDTO detailDTO)
		{
			return new InputReceiptDetail
			{
				Quantity = detailDTO.Quantity,
				Price = detailDTO.Price,
				ProductDetailId = _context.ProductDetails.Where(x => x.ColorId == detailDTO.ColorId && x.FabricId == detailDTO.FabricId && x.SizeId == detailDTO.SizeId).FirstOrDefault().DetailId
			};
		}
		public OutputReceiptDetail ToOutputReceiptDetail(CreateReceiptDetailDTO detailDTO)
		{
			return new OutputReceiptDetail
			{
				Quantity = detailDTO.Quantity,
				Price = detailDTO.Price,
				ProductDetailId = _context.ProductDetails.Where(x => x.ColorId == detailDTO.ColorId && x.FabricId == detailDTO.FabricId && x.SizeId == detailDTO.SizeId).FirstOrDefault().DetailId,
			};
        }
	}
}
