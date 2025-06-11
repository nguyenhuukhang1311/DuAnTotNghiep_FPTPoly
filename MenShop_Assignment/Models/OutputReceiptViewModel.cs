using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;

namespace MenShop_Assignment.Models
{
    public class OutputReceiptViewModel
    {
        public int ReceiptId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public OrderStatus? Status { get; set; }
        public decimal? Total { get; set; }
        public string? ManagerName { get; set; }
        public User? Manager { get; set; }
        public string? BranchName { get; set; }
        public Branch? Branch { get; set; }
        public ICollection<OutputReceiptDetailModel>? OutputReceiptDetails { get; set; }
    }
}
