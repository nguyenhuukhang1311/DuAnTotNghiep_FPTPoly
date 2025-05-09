using MenShop_Assignment.Datas;

namespace MenShop_Assignment.Models.BranchModel
{
    public class BranchProductDetailViewModel
    {
        public string? ProductName { get; set; }
        public string? Image { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string? ColorName { get; set; }
        public string? SizeName { get; set; }
        public string? FabricName { get; set; }
    }
}
