using MenShop_Assignment.Datas;

namespace MenShop_Assignment.Models.OutputReceiptView
{
	public class ReceiptDetailViewModel
    {
		public string? Name { get; set; }
		public string? Color { get; set; }
		public string? Size { get; set; }
		public string? Fabric {  get; set; }
		public int? Quantity { get; set; }
		public decimal? Price { get; set; }
	}
}
