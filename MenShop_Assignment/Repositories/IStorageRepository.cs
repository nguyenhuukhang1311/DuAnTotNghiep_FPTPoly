using MenShop_Assignment.Datas;
using MenShop_Assignment.Models;

namespace MenShop_Assignment.Repositories
{
    public interface IStorageRepository
    {
        Task<List<StorageViewModel>> GetAllStoragesAsync();
        Task<List<StorageDetailsViewModel>> GetDetailsByProductId(int productId);
        Task<List<ProductViewModel1>> GetProductsByStorageIdAsync(int storageId);

        Task<StorageDetail?> GetByProductIdAsync(int productDetailId);
        Task SaveChangesAsync();
    }
}
