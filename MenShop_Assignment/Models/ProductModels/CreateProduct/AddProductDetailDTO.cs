namespace MenShop_Assignment.Models.ProductModels.CreateProduct
{
    public class AddProductDetailDTO
    {
        public int ProductId { get; set; }
        public List<ProductDetailItemDTO> Details { get; set; }
    }
}
