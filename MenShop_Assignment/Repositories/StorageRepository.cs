using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models;
using MenShop_Assignment.Mapper.MapperProduct;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;

namespace MenShop_Assignment.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductMapper _productMapper;
        private readonly StorageDetailMapper _storageDetailMapper;
        public StorageRepository(ApplicationDbContext context,ProductMapper productMapper, StorageDetailMapper storageDetailMapper)
        {
            _context = context;
            _productMapper = productMapper;
            _storageDetailMapper = storageDetailMapper;
        }
        public async Task<List<StorageViewModel>> GetAllStoragesAsync()
        {
            var storages = await _context.Storages.Include(s => s.CategoryProduct).Include(s => s.Manager).Include(s => s.StorageDetails)
                    .ThenInclude(sd => sd.ProductDetail)
                        .ThenInclude(pd => pd.Product)
                .ToListAsync();

            return storages.Select(StorageMapper.ToStorageDto).ToList();
        }
        public async Task<List<ProductViewModel1>> GetProductsByStorageIdAsync(int storageId)
        {
            var products = await _context.Products
                   .Where(p => p.ProductDetails!.Any(pd =>
                       pd.StorageDetails!.Any(sd => sd.StorageId == storageId)))
                   .Include(p => p.Category)
                   .Include(p => p.ProductDetails!).ThenInclude(pd => pd.StorageDetails!
                       .Where(sd => sd.StorageId == storageId))
                   .ToListAsync();
            List<ProductViewModel1> productViewModels = products.Select(x => _productMapper.ToProductStorageView(x)).ToList();
            return productViewModels;
        }
        public async Task<List<StorageDetailsViewModel>> GetDetailsByProductId(int productId)
        {
                var storageDetails = await _context.StorageDetails
                        .Where(sd => sd.ProductDetail != null && 
                               sd.ProductDetail.ProductId == productId)
                        .Include(sd => sd.ProductDetail)
                        .Include(sd => sd.Storage)
                        .ToListAsync();
            List<StorageDetailsViewModel> storageDetailsViewModels = storageDetails.Select(x => _storageDetailMapper.ToStorageDetailView(x)).ToList();
            return storageDetailsViewModels;
        }

        public async Task<StorageDetail?> GetByProductIdAsync(int productDetailId)
        {
            return await _context.StorageDetails
                .FirstOrDefaultAsync(s => s.ProductDetailId == productDetailId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
