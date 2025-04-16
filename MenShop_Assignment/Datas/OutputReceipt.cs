namespace MenShop_Assignment.Datas
{
    public class OutputReceipt
    {
        public int ReceiptId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? ConfirmedDate { get; set; } 
        public bool? IsCancelled { get; set; }
        public bool? IsDelivering { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsConfirmed { get; set; }
        public decimal? Total { get; set; }
        public string? ManagerId { get; set; }
        public User? Manager { get; set; }
        public int BranchId { get; set; }
        public Branch? Branch { get; set; }
        public ICollection<OutputReceiptDetail>? OutputReceiptDetails { get; set; }
    }
}
