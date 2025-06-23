using MenShop_Assignment.Datas;
using MenShop_Assignment.DTOs;
using MenShop_Assignment.Models;
using MenShop_Assignment.Models.ProductModels.ReponseDTO;

namespace MenShop_Assignment.Mapper
{
	public static class ProductMapper
	{
		public static ProductViewModel ToProductViewModel(Product product)
		{
			return new ProductViewModel
			{
				ProductId = product.ProductId == null ? 0 : product.ProductId,
				ProductName = product.ProductName ?? null,
				CategoryProduct = product.Category?.Name ?? null,
				Description = product.Description ?? null,
				Status = product.Status.ToString() ?? null,
				Thumbnail = product.ProductDetails?.FirstOrDefault()?.Images?.FirstOrDefault()?.FullPath ?? null,
				ProductDetails = product.ProductDetails?.Select(ToProductDetailViewModel)?.ToList() ?? []
			};
		}
		public static ProductDetailViewModel ToProductDetailViewModel(ProductDetail productDetail) 
		{
			return new ProductDetailViewModel
			{
				DetailId = productDetail.DetailId,
				ColorName = productDetail.Color?.Name ?? null,
				SizeName = productDetail.Size?.Name ?? null,
				FabricName = productDetail.Fabric?.Name ?? null,
				ProductName = productDetail.Product?.ProductName ?? null,
				Images = productDetail?.Images?.Select(x => x.FullPath).ToList() ?? [],
			};
		}
		public static ImageProductViewModel ToImageProductViewModel(ImagesProduct image)
		{
			return new ImageProductViewModel
			{
				FullPath = string.IsNullOrEmpty(image.FullPath)
					? $"http://localhost:5014/StaticFiles/Images/{image.Path}"
					: image.FullPath,
				ProductDetailId = image.ProductDetailId,
				Id = image.Id
			};
		}
		public static ProductResponseDTO ToCreateAndUpdateProductResponse(Product product)
		{
			return new ProductResponseDTO
			{
				ProductId = product.ProductId,
				ProductName = product.ProductName ?? string.Empty,
				Description = product.Description ?? string.Empty,
				CategoryId = product.CategoryId ?? 0,
				Status = product.Status ?? false,
			};
		}
		public static ProductDetailDTO ToCreateProductDetailResponse(ProductDetail detail)
		{
			return new ProductDetailDTO
			{
				DetailId = detail.DetailId,
				ProductId = detail.ProductId,
				SizeId = detail.SizeId,
				ColorId = detail.ColorId,
				FabricId = detail.FabricId
			};
		}

		public static CreateImageResponse ToCreateImageResponse(ImagesProduct image)
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
