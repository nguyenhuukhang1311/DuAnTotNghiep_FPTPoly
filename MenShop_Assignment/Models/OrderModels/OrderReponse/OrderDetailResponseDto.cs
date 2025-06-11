using MenShop_Assignment.Models.ProductModels.CreateProduct;

namespace MenShop_Assignment.Models.OrderModels.OrderReponse
{
    public class OrderDetailResponseDto
    {
        public int ProductDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        //public ProductDetailDto ProductDetail { get; set; }
    }

}
