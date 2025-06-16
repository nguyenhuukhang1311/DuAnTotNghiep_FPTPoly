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
<<<<<<< HEAD
        public OrderStatus? Status { get; set; }
        public decimal? Total { get; set; }
        public string? ManagerName { get; set; }
        public User? Manager { get; set; }
        public string? BranchName { get; set; }
        public Branch? Branch { get; set; }
        public ICollection<OutputReceiptDetailModel>? OutputReceiptDetails { get; set; }
=======
        public string? Status { get; set; }
        public decimal? Total { get; set; }
        public string? ManagerName { get; set; }
        public string BranchName { get; set; }
        public ICollection<ReceiptDetailViewModel>? OutputReceiptDetails { get; set; }
>>>>>>> 4c7a32a113c1670ac083587cc24696b9b1623ec9
    }
}
