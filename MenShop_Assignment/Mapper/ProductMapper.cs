using MenShop_Assignment.Datas;
using MenShop_Assignment.Models;

namespace MenShop_Assignment.Mapper
{
    public class ProductMapper
    {
        private readonly ApplicationDbContext _context;
        public ProductMapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public ProductViewModel ToProductStorageView(Product product)
        {
            int productDetailId = _context.ProductDetails.Where(x => x.ProductId == product.ProductId).FirstOrDefault().DetailId;
            return new ProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Status = product.Status,
                CategoryName=_context.CategoryProducts.Where(x=>x.CategoryId==product.CategoryId).FirstOrDefault().Name,
                ImageUrls=_context.ImagesProducts.Where(x=>x.ProductDetailId==productDetailId).FirstOrDefault().FullPath,
            };
        }
    }
}
