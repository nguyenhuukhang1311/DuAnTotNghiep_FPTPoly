using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.CategoryModels;

namespace MenShop_Assignment.Repositories.Category
{
    public interface ICategoryProductRepository
    {
        Task<IEnumerable<CategoryModelView>> GetAllCategoriesAsync();
        Task<CategoryModelView> GetCategoryByIdAsync(int id);
        Task<CategoryModelView> CreateCategoryAsync(CategoryModelView category);
        Task<CategoryModelView> UpdateCategoryAsync(CategoryModelView category);
        Task<bool> RemoveCategoryAsync(int id);
    }
}
