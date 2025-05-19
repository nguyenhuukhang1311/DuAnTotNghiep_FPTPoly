﻿namespace MenShop_Assignment.Models.ProductModels.UpdateProduct
{
    public class UpdateProductDTO
    {
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
        public int CategoryId { get; set; }

        public List<UpdateProductDetailDTO> ProductDetails { get; set; } = new();
    }
}
