﻿namespace MenShop_Assignment.Models.ProductModels.ReponseDTO
{
    public class CreateImageResponse
    {
        public int ImageId { get; set; }
        public int ProductDetailId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
