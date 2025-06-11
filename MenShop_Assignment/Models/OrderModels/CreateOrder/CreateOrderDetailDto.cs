namespace MenShop_Assignment.Models.OrderModels.CreateOrder
{
    public class CreateOrderDetailDto
    {
        public int ProductDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
