using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper.MapperProduct;
using MenShop_Assignment.Models.ProductModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace MenShop_Assignment.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductMapper _mapper;

        public ProductRepository(ApplicationDbContext context, ProductMapper productMapper)
        {
            _context = context;
            _mapper = productMapper;
        }
        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductDetails)
                    .ThenInclude(pd => pd.StorageDetails)
                        .ThenInclude(sd => sd.Storage) // Lấy thông tin kho
                .Include(p => p.ProductDetails)
                    .ThenInclude(pd => pd.BranchDetails) // Lấy chi tiết chi nhánh
                        .ThenInclude(bd => bd.Branch) // Lấy thông tin chi nhánh
                .Select(p => ProductMapper.ToProductViewModel(p))
                .ToListAsync();

            return products;
        }


        public async Task<ProductViewModel?> GetProductByIdAsync(int productId)
        {
            var product = await _context.Products
                .Include(p => p.Category) 
                .Include(p => p.ProductDetails) 
                .ThenInclude(pd => pd.StorageDetails) 
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                return null; // Không tìm thấy sản phẩm
            }
            return ProductMapper.ToProductViewModel(product);
        }
    }
}
