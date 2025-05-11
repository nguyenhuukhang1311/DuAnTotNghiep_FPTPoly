using MenShop_Assignment.Datas;

namespace MenShop_Assignment.Models
{
    public class StorageViewModel
    {
        public int StorageId { get; set; }
        public int? CategoryId { get; set; }
        public CategoryProduct? CategoryProduct { get; set; }
        public string? ManagerName { get; set; }
        public User? Manager { get; set; }
        public ICollection<StorageDetailsViewModel>? StorageDetails { get; set; }
        public ICollection<InputReceiptViewModel>? InputReceipts { get; set; }
        public ICollection<OutputReceiptViewModel>? OutputReceipts { get; set; }
    }
}
