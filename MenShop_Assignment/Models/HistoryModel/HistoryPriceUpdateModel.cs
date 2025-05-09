using MenShop_Assignment.Datas;

namespace MenShop_Assignment.Models.HistoryModel
{
    public class HistoryPriceUpdateModel
    {

        public int ProductDetailId { get; set; }
        public ProductDetail? ProductDetail { get; set; }
        public decimal? InputPrice { get; set; }
        public decimal? SellPrice { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
