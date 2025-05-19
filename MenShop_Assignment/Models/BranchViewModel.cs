using MenShop_Assignment.Datas;

namespace MenShop_Assignment.Models
{
    public class BranchViewModel
    {
        public int BranchId { get; set; }
        public string? Address { get; set; }
        public string? ManagerName { get; set; }
        public UserViewModel? Manager { get; set; }
        public ICollection<UserViewModel>? EmployeesList { get; set; }
        public ICollection<ReceiptDetailViewModel>? BranchDetails { get; set; }
        public ICollection<OutputReceiptViewModel>? OutputReceipts { get; set; }
    }
}
