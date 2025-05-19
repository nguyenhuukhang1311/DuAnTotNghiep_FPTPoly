namespace MenShop_Assignment.Models.ProductModels
{
    public class HistoryPriceViewModel
    {
        public decimal InputPrice { get; set; }
        public decimal OnlinePrice { get; set; }
        public decimal OfflinePrice { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
