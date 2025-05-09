using MenShop_Assignment.Datas;
using MenShop_Assignment.Models;

namespace MenShop_Assignment.Mapper
{
    public class StorageDetailMapper
    {
        private readonly ApplicationDbContext _context;
        public StorageDetailMapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public StorageDetailsViewModel ToStorageDetailView(StorageDetail storageDetail)
        {
            ProductDetail productDetail = _context.ProductDetails.Where(x => x.DetailId == storageDetail.ProductDetailId).FirstOrDefault();
            return new StorageDetailsViewModel
            {
                Quantity = storageDetail.Quantity,
                Price = storageDetail.Price,
                Name = _context.Products.Where(x => x.ProductId == productDetail.ProductId).FirstOrDefault().ProductName,
                Color = _context.Colors.Where(x => x.ColorId == productDetail.ColorId).FirstOrDefault().Name,
                Size = _context.Sizes.Where(x => x.SizeId == productDetail.SizeId).FirstOrDefault().Name,
                Fabric = _context.Fabrics.Where(x => x.FabricId == productDetail.FabricId).FirstOrDefault().Name,
                ImageUrl=_context.ImagesProducts.Where(x=>x.ProductDetailId==productDetail.DetailId).FirstOrDefault().FullPath,
            };
        }
    }
}
