using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.CategoryModels;
using MenShop_Assignment.Models.ProductModels.ReponseDTO;
using MenShop_Assignment.Models.ProductModels.ViewModel;

namespace MenShop_Assignment.Mapper.MapperProduct
{
    public class ProductMapper
    {

        public  IEnumerable<CategoryGroupViewModel> GroupByCategory(IEnumerable<Product> products)
        {
            return products
                .GroupBy(p => p.Category?.Name ?? "Không tồn tại")
                .Select(group => new CategoryGroupViewModel
                {
                    CategoryName = group.Key,
                    Products = group.Select(ToProductViewModel).ToList()
                });
        }

        // Ánh xạ Product -> ProductViewModel
        public ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                ProductId = product.ProductId,
                CategoryName = product.Category?.Name ?? string.Empty,
                ProductName = product.ProductName ?? string.Empty,
                Description = product.Description ?? string.Empty,
                Status = product.Status ?? false,
                ProductDetails = product.ProductDetails?
                    .Select(pd => ToProductDetailViewModel(pd))
                    .ToList() ?? new List<ProductDetailViewModel>()
            };
        }

        // Ánh xạ ProductDetail -> ProductDetailViewModel
        public ProductDetailViewModel ToProductDetailViewModel(ProductDetail detail)
        {

            return new ProductDetailViewModel
            {
                DetailId = detail.DetailId,
                ProductId = detail.ProductId,
                SizeName = detail.Size?.Name ?? string.Empty,
                ColorName = detail.Color?.Name ?? string.Empty,
                FabricName = detail.Fabric?.Name ?? string.Empty,
                HistoryPrices = detail.HistoryPrices?
                    .Select(hp => ToHistoryPriceViewModel(hp))
                    .ToList() ?? new List<HistoryPriceViewModel>(),
                Images = detail.Images?
                    .Select(img => ToImageProductViewModel(img))
                    .ToList() ?? new List<ImageProductViewModel>(),
                
            };
        }





        // Ánh xạ HistoryPrice -> HistoryPriceViewModel
        public HistoryPriceViewModel ToHistoryPriceViewModel(HistoryPrice price)
        {
            return new HistoryPriceViewModel
            {
                InputPrice = price.InputPrice ?? 0,
                OnlinePrice = price.OnlinePrice ?? 0,
                OfflinePrice = price.OfflinePrice ?? 0,
                SellPrice = price.SellPrice ?? 0,
                UpdatedDate = price.UpdatedDate ?? DateTime.MinValue
            };
        }

        // Ánh xạ ImagesProduct -> ImageProductViewModel
        public ImageProductViewModel ToImageProductViewModel(ImagesProduct image)
        {
            return new ImageProductViewModel
            {
                FullPath = string.IsNullOrEmpty(image.FullPath)
                    ? $"http://localhost:5014/StaticFiles/Images/{image.Path}"
                    : image.FullPath,
                ProductDetailId = image.ProductDetailId
            };
        }

        //reponse
        public ProductResponseDTO ToCreateProductResponse(Product product)
        {
            return new ProductResponseDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName ?? string.Empty,
                Description = product.Description ?? string.Empty,
                CategoryId = product.CategoryId ?? 0,
                Status = product.Status ?? false,
                ProductDetails = product.ProductDetails.Select(detail => new ProductDetailResponse
                {
                    ProductDetailId = detail.DetailId,
                    SizeId = detail.SizeId,
                    ColorId = detail.ColorId,
                    FabricId = detail.FabricId,
                    Images = detail.Images?.Select(img => new ImageResponse
                    {
                        ImageId = img.Id,
                        ImageUrl = string.IsNullOrEmpty(img.FullPath)
                            ? $"http://localhost:5014/StaticFiles/Images/{img.Path}"
                            : img.FullPath
                    }).ToList() ?? new List<ImageResponse>()
                }).ToList()
            };
        }
        public CreateProductResponse ToCreateOnlyProductResponse(Product product)
        {
            return new CreateProductResponse
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName ?? string.Empty,
                Description = product.Description ?? string.Empty,
                CategoryId = product.CategoryId ?? 0,
                Status = product.Status ?? false,
            };
        }
        public CreateProductDetailResponse ToCreateProductDetailResponse(ProductDetail detail)
        {
            return new CreateProductDetailResponse
            {
                ProductDetailId = detail.DetailId,
                ProductId = detail.ProductId,
                SizeId = detail.SizeId,
                ColorId = detail.ColorId,
                FabricId = detail.FabricId
            };
        }
        public CreateImageResponse ToCreateImageResponse(ImagesProduct image)
        {
            return new CreateImageResponse
            {
                ImageId = image.Id,
                ProductDetailId = image.ProductDetailId,
                ImageUrl = string.IsNullOrEmpty(image.FullPath)
                    ? $"http://localhost:5014/StaticFiles/Images/{image.Path}"
                    : image.FullPath
            };
        }

    }
}
