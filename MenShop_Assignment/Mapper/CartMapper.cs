﻿using MenShop_Assignment.Datas;
using MenShop_Assignment.Models;

namespace MenShop_Assignment.Mapper
{
	public static class CartMapper
	{
		public static CartViewModel ToCartViewModel(Cart cart)
		{
			return new CartViewModel
			{
				CartId = cart.CartId,
				CreatedDate = cart.CreatedDate ?? null,
				CustomerName = cart.Customer?.UserName ?? null,
				Details = cart.Details.Select(ToCartDetailViewModel).ToList() ?? [],
			};
		}
		public static ProductDetailViewModel ToCartDetailViewModel(CartDetail cartDetail)
		{
			return new ProductDetailViewModel
			{
				DetailId = cartDetail.ProductDetailId.Value,
				ProductName = cartDetail.ProductDetail?.Product?.ProductName ?? null,
				FabricName = cartDetail.ProductDetail?.Fabric?.Name ?? null,
				ColorName = cartDetail.ProductDetail?.Color?.Name ?? null,
				SizeName = cartDetail.ProductDetail?.Size?.Name ?? null,
				SellPrice = cartDetail.Price ?? 0,
				Images = cartDetail.ProductDetail?.Images?.Select(x => x.FullPath).ToList() ?? [],
				Quantity = cartDetail.Quantity ?? 0,
			};
		}
	}
}
