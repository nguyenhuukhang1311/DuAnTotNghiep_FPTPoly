using MenShop_Assignment.Datas;
using MenShop_Assignment.Models;
using MenShop_Assignment.Models.BranchModel;

namespace MenShop_Assignment.Mapper
{
    public class BranchMapper
    {
        private readonly ApplicationDbContext _context;
        public BranchMapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public BranchProductViewModel ToDto(BranchDetail branchDetail)
        {
            return new BranchProductViewModel
            {
                ProductId = branchDetail.ProductDetail?.ProductId ?? null,
                ProductName = branchDetail.ProductDetail?.Product?.ProductName ?? "",
                Image = _context.ImagesProducts.Where(x=>x.ProductDetailId == branchDetail.ProductDetailId).FirstOrDefault().FullPath,
                Description = branchDetail.ProductDetail?.Product?.Description ?? "",
            };
        }

        public BranchProductDetailViewModel ToDetailDto(BranchDetail branchDetail)
        {
            return new BranchProductDetailViewModel
            {
                ProductName = branchDetail.ProductDetail?.Product?.ProductName ?? "",
                Price = branchDetail.Price,
                Image = _context.ImagesProducts.Where(x => x.ProductDetailId == branchDetail.ProductDetailId).FirstOrDefault().FullPath,
                Quantity = branchDetail.Quantity,
                ColorName = branchDetail.ProductDetail?.Color?.Name ?? "",
                SizeName = branchDetail.ProductDetail?.Size?.Name ?? "",
                FabricName = branchDetail.ProductDetail?.Fabric?.Name ?? ""
            };
        }
    }
}
