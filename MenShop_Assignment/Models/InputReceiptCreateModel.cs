using MenShop_Assignment.Datas;
using MenShop_Assignment.Extensions;

namespace MenShop_Assignment.Models
{
	public class InputReceiptViewModel
	{
		public int ReceiptId { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? CancelDate { get; set; }
		public DateTime? ConfirmedDate { get; set; }
		public string? Status { get; set; }
		public decimal? Total { get; set; }
		public string? ManagerName { get; set; }
		public User? Manager { get; set; }
		public int StorageId { get; set; }
		public Storage? Storage { get; set; }
		public ICollection<InputReceiptDetailViewModel>? InputReceiptDetails { get; set; }
	}
}
