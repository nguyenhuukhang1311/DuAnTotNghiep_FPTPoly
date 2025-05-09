using MenShop_Assignment.Datas;

namespace MenShop_Assignment.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public bool? Status { get; set; }
        public string? ImageUrls { get; set; }

    }
}
