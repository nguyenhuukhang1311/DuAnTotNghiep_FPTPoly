﻿namespace MenShop_Assignment.Models.ProductModels.ReponseDTO
{
    public class ImageResponse
    {
		public int ImageId { get; set; }
		public string ImageUrl { get; set; }
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
	}
}
