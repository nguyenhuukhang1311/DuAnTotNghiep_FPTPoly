using MenShop_Assignment.Datas;
using MenShop_Assignment.Models;

namespace MenShop_Assignment.Mapper
{
    public static class StorageMapper
    {
        //private readonly ApplicationDbContext _context;
        //public StorageMapper(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        public static StorageViewModel ToStorageDto(Storage storage)
        {
            return new StorageViewModel
            {
                StorageId = storage.StorageId,
                CategoryId = storage.CategoryProduct?.CategoryId,
                ManagerName = storage.Manager?.ManagerId,
                StorageDetails = storage.StorageDetails?.Select(sd => new StorageDetailsViewModel
                {
                    Name = sd.ProductDetail?.Product?.ProductName,
                    Price = sd.Price,
                    Quantity = sd.Quantity
                }).ToList()
            };
        }
    }
}
