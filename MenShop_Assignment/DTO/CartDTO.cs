namespace MenShop_Assignment.DTO
{
    public class CartDetailDTO
    {
        public int ProductDetailId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice => Price * Quantity;
    }

    public class CartDTO
    {
        public Guid CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<CartDetailDTO> Details { get; set; } = new();

        public decimal TotalCartPrice => Details.Sum(d => d.TotalPrice); 
    }
}
