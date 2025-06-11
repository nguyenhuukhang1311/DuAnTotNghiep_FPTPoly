using MenShop_Assignment.Models.ProductModels.ViewModel;

namespace MenShop_Assignment.Models.CategoryModels
{
    public class CategoryGroupViewModel
    {
        public string CategoryName { get; set; } = string.Empty;
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}
