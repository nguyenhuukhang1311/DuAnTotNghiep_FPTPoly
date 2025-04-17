namespace MenShop_Assignment.Datas
{
    public class HistoryPrice
    {
        public int Id { get; set; }
        public int ProductDetailId { get; set; }
        public ProductDetail? ProductDetail { get; set; }
        public decimal? InputPrice { get; set; }
        public decimal? OnlinePrice { get; set; }
        public decimal? OfflinePrice { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
