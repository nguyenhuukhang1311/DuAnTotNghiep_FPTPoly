using MenShop_Assignment.Datas;

namespace MenShop_Assignment.Models
{
    public class StorageViewModel
    {
        public int StorageId { get; set; }
        public string? Category { get; set; }
        public CategoryProduct? CategoryProduct { get; set; }
        public string? ManagerName { get; set; }
        public UserViewModel? Manager { get; set; }
        public ICollection<ReceiptDetailViewModel>? StorageDetails { get; set; }
        public ICollection<InputReceiptViewModel>? InputReceipts { get; set; }
        public ICollection<OutputReceiptViewModel>? OutputReceipts { get; set; }
    }
}
