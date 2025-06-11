namespace MenShop_Assignment.Models.ProductModels
{
    public class StorageViewModel
    {
        public string StorageName { get; set; } = string.Empty;
        public List<StorageDetailViewModel> Details { get; set; } = new();
    }
}
