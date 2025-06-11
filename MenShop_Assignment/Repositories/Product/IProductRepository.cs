using MenShop_Assignment.Models.CategoryModels;
using MenShop_Assignment.Models.ProductModels.CreateProduct;
using MenShop_Assignment.Models.ProductModels.ReponseDTO;
using MenShop_Assignment.Models.ProductModels.UpdateProduct;
using MenShop_Assignment.Models.ProductModels.ViewModel;

namespace MenShop_Assignment.Repositories.Product
{
    public interface IProductRepository
    {
        Task<IEnumerable<CategoryGroupViewModel>> GetAllProductsAsync();
        Task<ProductViewModel?> GetProductByIdAsync(int productId);
        Task<IEnumerable<CategoryGroupViewModel>> GetProductByCategoryAsync(int categoryId);
        Task<ProductResponseDTO> CreateProductAsync(CreateProductDTO createProductDTO);
        Task<CreateProductResponse> CreateProductOnlyAsync(CreateProductOnlyDTO dto);
        Task<CreateProductDetailResponse> AddProductDetailsAsync(AddProductDetailDTO dto);
        Task<CreateImageResponse> AddImagesToDetailAsync(int detailId, List<string> imageUrls);
        Task<ProductResponseDTO> UpdateProductAsync(int productId, UpdateProductDTO updateProductDTO);
        Task DeleteProductDetailAsync(int detailId);
        Task DeleteProductAsync(int productId);
    }
}
