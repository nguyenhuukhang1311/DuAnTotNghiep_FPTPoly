using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.ProductModels;

namespace MenShop_Assignment.Mapper.MapperProduct
{
    public class ProductMapper
    {
        // Ánh xạ Product -> ProductViewModel
        public static ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName ?? string.Empty,
                Description = product.Description ?? string.Empty,
                Status = product.Status ?? false,
                CategoryName = product.Category?.Name ?? string.Empty,
                ProductDetails = product.ProductDetails?
                    .Select(pd => ToProductDetailViewModel(pd))
                    .ToList() ?? new List<ProductDetailViewModel>()
            };
        }

        // Ánh xạ ProductDetail -> ProductDetailViewModel
        public static ProductDetailViewModel ToProductDetailViewModel(ProductDetail detail)
        {
            // Gom nhóm chi tiết kho theo tên danh mục (StorageName)
            var storageViews = detail.StorageDetails?
                .GroupBy(sd => sd.Storage?.CategoryProduct?.Name ?? "Không xác định")
                .Select(group => new StorageViewModel
                {
                    StorageName = group.Key,
                    Details = group.Select(sd => new StorageDetailViewModel
                    {
                        SizeName = detail.Size?.Name ?? string.Empty,
                        ColorName = detail.Color?.Name ?? string.Empty,
                        FabricName = detail.Fabric?.Name ?? string.Empty,
                        Quantity = sd.Quantity ?? 0
                    }).ToList()
                }).ToList() ?? new List<StorageViewModel>();

            // Ánh xạ chi tiết theo kho (Branch)
            var branchDetails = detail.BranchDetails?
                .Select(bd => new BranchDetailViewModel
                {
                    BranchId = bd.BranchId,
                    Price = bd.Price ?? 0,
                    Quantity = bd.Quantity ?? 0
                }).ToList() ?? new List<BranchDetailViewModel>();

            return new ProductDetailViewModel
            {
                DetailId = detail.DetailId,
                ProductId = detail.ProductId,
                SizeName = detail.Size?.Name ?? string.Empty,
                ColorName = detail.Color?.Name ?? string.Empty,
                FabricName = detail.Fabric?.Name ?? string.Empty,
                ToTalQuantityInStorage = storageViews
                    .SelectMany(s => s.Details)
                    .Sum(d => d.Quantity)
                    .ToString(),
                StorageQuantities = storageViews,
                BranchDetails = branchDetails,
                HistoryPrices = detail.HistoryPrices?
                    .Select(hp => ToHistoryPriceViewModel(hp))
                    .ToList() ?? new List<HistoryPriceViewModel>(),
                Images = detail.Images?
                    .Select(img => ToImageProductViewModel(img))
                    .ToList() ?? new List<ImageProductViewModel>()
            };
        }





        // Ánh xạ HistoryPrice -> HistoryPriceViewModel
        public static HistoryPriceViewModel ToHistoryPriceViewModel(HistoryPrice price)
        {
            return new HistoryPriceViewModel
            {
                InputPrice = price.InputPrice ?? 0,
                OnlinePrice = price.OnlinePrice ?? 0,
                OfflinePrice = price.OfflinePrice ?? 0,
                UpdatedDate = price.UpdatedDate ?? DateTime.MinValue
            };
        }

        // Ánh xạ ImagesProduct -> ImageProductViewModel
        public static ImageProductViewModel ToImageProductViewModel(ImagesProduct image)
        {
            return new ImageProductViewModel
            {
                FullPath = string.IsNullOrEmpty(image.FullPath)
                    ? $"http://localhost:5014/StaticFiles/Images/{image.Path}"
                    : image.FullPath
            };
        }
    }
}
