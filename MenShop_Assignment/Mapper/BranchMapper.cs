using MenShop_Assignment.Datas;
using MenShop_Assignment.DTOs;
using MenShop_Assignment.Models;

namespace MenShop_Assignment.Mapper
{
    public static class BranchMapper
    {
        public static BranchViewModel ToBranchViewModel(Branch branch)
        {
            return new BranchViewModel
            {
                BranchId = branch.BranchId,
                Address = branch.Address ?? null,
                ManagerName = branch.Manager?.FullName ?? null,
                BranchDetails = branch.BranchDetails?.Select(ToBranchDetailViewModel).ToList() ?? [],
            };
        }
        public static ProductDetailViewModel ToBranchDetailViewModel(BranchDetail branchDetail)
        {
            return new ProductDetailViewModel
            {
                DetailId = branchDetail.ProductDetailId,
                ProductName = branchDetail.ProductDetail?.Product?.ProductName ?? null,
                FabricName = branchDetail.ProductDetail?.Fabric?.Name ?? null,
                ColorName = branchDetail.ProductDetail?.Color?.Name ?? null,
                SizeName = branchDetail.ProductDetail?.Size?.Name ?? null,
                SellPrice = branchDetail.Price ?? null,
				Images = branchDetail.ProductDetail?.Images?.Select(x => x.FullPath).ToList() ?? [],
                Quantity = branchDetail.Quantity ?? null,
            };
        }
        public static Branch ToBranch(CreateUpdateBranchDTO branchDTO)
        {
            return new Branch
            {
                Address = branchDTO.Address ?? null,
            };
        }
    }
}
