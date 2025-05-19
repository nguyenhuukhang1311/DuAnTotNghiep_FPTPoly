using MenShop_Assignment.Models.ProductModels;

namespace MenShop_Assignment.Repositories.Product
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
        Task<ProductViewModel?> GetProductByIdAsync(int productId);
    }
}
