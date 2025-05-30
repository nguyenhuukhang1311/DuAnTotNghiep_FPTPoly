namespace MenShop_Assignment.DTO
{
    public class FailedOrderDto
    {
        public int OrderId { get; set; }
        public string Error { get; set; } = string.Empty;
    }
}
