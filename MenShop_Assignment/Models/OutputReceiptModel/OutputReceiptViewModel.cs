using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;

namespace MenShop_Assignment.Models.OutputReceiptView
{
    public class OutputReceiptViewModel
    {
        public int ReceiptId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public string? Status { get; set; }
        public decimal? Total { get; set; }
        public ICollection<ReceiptDetailViewModel>? OutputReceiptDetails { get; set; }
    }
}
