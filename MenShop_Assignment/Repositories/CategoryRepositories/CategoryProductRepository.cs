using MenShop_Assignment.Datas;
using MenShop_Assignment.DTOs;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Repositories.Category
{
    public class CategoryProductRepository : ICategoryProductRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryProductRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<CategoryProductViewModel>?> GetAllCategoriesAsync()
        {
            var categoryList = await _context.CategoryProducts.ToListAsync();
            if (categoryList == null) 
                return null;
            return categoryList.Select(CategoryProductMapper.ToCategoryProductView).ToList();
        }

        public async Task<CategoryProductViewModel?> GetCategoryByIdAsync(int id)
        {
            var categoryProduct = await _context.CategoryProducts.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (categoryProduct == null) 
                return null;

            return CategoryProductMapper.ToCategoryProductView(categoryProduct);
        }

        public async Task<bool> CreateCategoryAsync(CreateUpdateCategoryDTO categoryDTO)
        {
            if (categoryDTO == null|| categoryDTO.Name=="")
                return false;

            _context.CategoryProducts.Add(CategoryProductMapper.ToCategoryProduct(categoryDTO));
            await _context.SaveChangesAsync();

            _context.Storages.Add(new Storage{CategoryId = categoryDTO.Id});
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateCategoryAsync(CreateUpdateCategoryDTO categoryDTO)
        {
            var existing = await _context.CategoryProducts.FirstOrDefaultAsync(x => x.CategoryId == categoryDTO.Id);
            if (existing == null) 
                return false;

            existing.Name = categoryDTO.Name;
            _context.CategoryProducts.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveCategoryAsync(int Id)
        {
            var category = await _context.CategoryProducts.FirstOrDefaultAsync(x => x.CategoryId == Id);
            if (category == null) 
                return false;

            var storage = await _context.Storages.FirstOrDefaultAsync(s => s.CategoryId == Id);

			_context.Storages.Remove(storage);
			await _context.SaveChangesAsync();

			_context.CategoryProducts.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
