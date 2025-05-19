using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper.MapperCategory;
using MenShop_Assignment.Models;
using MenShop_Assignment.Models.CategoryModels;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Repositories.Category
{
    public class CategoryProductRepository : ICategoryProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly CategoryProductMapper _mapper;

        public CategoryProductRepository(ApplicationDbContext dbContext, CategoryProductMapper categoryProductMapper)
        {
            _context = dbContext;
            _mapper = categoryProductMapper;
        }

        public async Task<IEnumerable<CategoryModelView>> GetAllCategoriesAsync()
        {
            var entities = await _context.CategoryProducts.ToListAsync();
            return entities.Select(x => _mapper.ToCategoryProductView(x));
        }

        public async Task<CategoryModelView?> GetCategoryByIdAsync(int id)
        {
            var categoryProduct = await _context.CategoryProducts
                                                .FirstOrDefaultAsync(x => x.CategoryId == id);
            if (categoryProduct == null) return null;

            return _mapper.ToCategoryProductView(categoryProduct);
        }

        public async Task<CategoryModelView> CreateCategoryAsync(CategoryModelView category)
        {
            var categoryProduct = new CategoryProduct
            {
                Name = category.Name
            };

            _context.CategoryProducts.Add(categoryProduct);
            await _context.SaveChangesAsync();

            var storage = new Storage
            {
                CategoryId = categoryProduct.CategoryId
            };

            _context.Storages.Add(storage);
            await _context.SaveChangesAsync();

            return category;
        }



        public async Task<CategoryModelView> UpdateCategoryAsync(CategoryModelView category)
        {
            var existing = await _context.CategoryProducts
                                         .FirstOrDefaultAsync(x => x.CategoryId == category.CategoryId);
            if (existing == null) return null;

            existing.Name = category.Name;
            _context.CategoryProducts.Update(existing);
            await _context.SaveChangesAsync();

            return _mapper.ToCategoryProductView(existing);
        }

        public async Task<bool> RemoveCategoryAsync(int id)
        {
            var category = await _context.CategoryProducts.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (category == null) return false;

            // Không cho xóa nếu có sản phẩm thuộc danh mục này
            var hasProducts = await _context.Products.AnyAsync(p => p.CategoryId == id);
            if (hasProducts) return false;

            // Tìm kho tương ứng với danh mục
            var storage = await _context.Storages.FirstOrDefaultAsync(s => s.CategoryId == id);

            // Nếu tồn tại kho thì kiểm tra chi tiết kho
            if (storage != null)
            {
                var hasStorageDetails = await _context.StorageDetails.AnyAsync(sd => sd.StorageId == storage.StorageId);
                if (hasStorageDetails) return false;

                _context.Storages.Remove(storage);
            }

            _context.CategoryProducts.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
