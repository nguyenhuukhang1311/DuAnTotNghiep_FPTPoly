namespace MenShop_Assignment.Models.ProductModels
{
    public class ProductDetailViewModel
    {
        public int DetailId { get; set; }
        public int ProductId { get; set; }
        public string SizeName { get; set; }
        public string ColorName { get; set; }
        public string FabricName { get; set; }
        public string ToTalQuantityInStorage { get; set; }
        public List<StorageViewModel> StorageQuantities { get; set; } = new List<StorageViewModel>();
        public List<HistoryPriceViewModel> HistoryPrices { get; set; }
        public List<ImageProductViewModel> Images { get; set; }
        public List<BranchDetailViewModel> BranchDetails { get; set; }

    }
}
