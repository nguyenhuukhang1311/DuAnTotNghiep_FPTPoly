using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper.MapperProduct;
using MenShop_Assignment.Models.CategoryModels;
using MenShop_Assignment.Models.ProductModels.CreateProduct;
using MenShop_Assignment.Models.ProductModels.ReponseDTO;
using MenShop_Assignment.Models.ProductModels.UpdateProduct;
using MenShop_Assignment.Models.ProductModels.ViewModel;
using MenShop_Assignment.Repositories.Product;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ProductMapper _mapper;

    public ProductRepository(ApplicationDbContext context, ProductMapper productMapper)
    {
        _context = context;
        _mapper = productMapper;
    }

    private IQueryable<Product> GetProductQuery()
    {
        return _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductDetails)
                .ThenInclude(d => d.Size)
            .Include(p => p.ProductDetails)
                .ThenInclude(d => d.Color)
            .Include(p => p.ProductDetails)
                .ThenInclude(d => d.Fabric)
            .Include(p => p.ProductDetails)
                .ThenInclude(d => d.HistoryPrices)
            .Include(p => p.ProductDetails)
                .ThenInclude(d => d.Images)
                    .AsSplitQuery();
    }

    // Lấy tất cả sản phẩm
    public async Task<IEnumerable<CategoryGroupViewModel>> GetAllProductsAsync()
    {
        var products = await GetProductQuery().ToListAsync();
        return _mapper.GroupByCategory(products);
    }
    public async Task<ProductViewModel?> GetProductByIdAsync(int productId)
    {
        var product = await GetProductQuery()
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        if (product == null)
        {
            return null;
        }

        return _mapper.ToProductViewModel(product);
    }

    public async Task<IEnumerable<CategoryGroupViewModel>> GetProductByCategoryAsync(int categoryId)
    {
        var products = await GetProductQuery()
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();

        return _mapper.GroupByCategory(products);
    }

    public async Task<ProductResponseDTO> CreateProductAsync(CreateProductDTO dto)
    {
        var response = new ProductResponseDTO();
        try
        {
            // 1. Validate category
            await ValidateCategoryExistsAsync(dto.CategoryId);

            // 2. Tạo product
            var product = new Product
            {
                ProductName = dto.ProductName,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                Status = dto.Status,
                ProductDetails = new List<ProductDetail>()
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync(); // lưu để có ProductId

            // 3. Thêm từng detail
            foreach (var det in dto.ProductDetails)
            {
                // Validate size, color, fabric
                await ValidateProductDetailReferencesAsync(det.SizeId, det.ColorId, det.FabricId);

                var pd = new ProductDetail
                {
                    SizeId = det.SizeId,
                    ColorId = det.ColorId,
                    FabricId = det.FabricId,
                    ProductId = product.ProductId
                };
                _context.ProductDetails.Add(pd);
                await _context.SaveChangesAsync(); // lưu để có DetailId

                // Thêm ảnh nếu có
                if (det.Images != null && det.Images.Any())
                {
                    foreach (var imgDto in det.Images)
                    {
                        var img = new ImagesProduct
                        {
                            FullPath = imgDto.ImageUrl,
                            ProductDetailId = pd.DetailId
                        };
                        _context.ImagesProducts.Add(img);
                    }
                    await _context.SaveChangesAsync();
                }
            }

            // 4. Map và trả về
            response = _mapper.ToCreateProductResponse(product);
            response.IsSuccess = true;
            response.Message = "Tạo sản phẩm thành công";
        }
        catch (DbUpdateException dbEx)
        {
            response.IsSuccess = false;
            response.Message = "Lỗi khi lưu vào CSDL: " + (dbEx.InnerException?.Message ?? dbEx.Message);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<ProductResponseDTO> UpdateProductAsync(int productId, UpdateProductDTO dto)
    {
        var response = new ProductResponseDTO();
        try
        {
            // 1. Validate category
            await ValidateCategoryExistsAsync(dto.CategoryId);

            // 2. Lấy product
            var product = await _context.Products
                .Include(p => p.ProductDetails)
                    .ThenInclude(d => d.Images)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
                return new ProductResponseDTO
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy sản phẩm!"
                };

            // 3. Cập nhật product
            product.ProductName = dto.ProductName;
            product.Description = dto.Description;
            product.Status = dto.Status;
            product.CategoryId = dto.CategoryId;

            // 4. Cập nhật chi tiết + ảnh
            foreach (var det in dto.ProductDetails)
            {
                await ValidateProductDetailReferencesAsync(det.SizeId, det.ColorId, det.FabricId);
                var existingDet = product.ProductDetails
                    .FirstOrDefault(d => d.DetailId == det.DetailId);
                if (existingDet == null) continue;

                existingDet.SizeId = det.SizeId;
                existingDet.ColorId = det.ColorId;
                existingDet.FabricId = det.FabricId;

                var imgs = existingDet.Images.ToList();
                foreach (var imgDto in det.Images)
                {
                    if (imgDto.ImageId > 0)
                    {
                        await ValidateImageExistsAsync(imgDto.ImageId, existingDet.DetailId);
                        var img = imgs.First(i => i.Id == imgDto.ImageId);
                        if (!string.Equals(img.FullPath, imgDto.ImageUrl, StringComparison.OrdinalIgnoreCase))
                            img.FullPath = imgDto.ImageUrl;
                    }
                }
            }

            // 5. Lưu và map về DTO
            await _context.SaveChangesAsync();
            response = _mapper.ToCreateProductResponse(product);
            response.IsSuccess = true;
            response.Message = "Cập nhật sản phẩm thành công";
        }
        catch (DbUpdateException dbEx)
        {
            response.IsSuccess = false;
            response.Message = "Lỗi khi lưu vào CSDL: " + (dbEx.InnerException?.Message ?? dbEx.Message);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }


    ///tách nhỏ
    public async Task<CreateProductResponse> CreateProductOnlyAsync(CreateProductOnlyDTO dto)
    {
        var response = new CreateProductResponse();
        try
        {
            await ValidateCategoryExistsAsync(dto.CategoryId);

            var product = new Product
            {
                ProductName = dto.ProductName,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                Status = dto.Status
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            response = _mapper.ToCreateOnlyProductResponse(product);
            response.IsSuccess = true;
            response.Message = "Tạo sản phẩm thành công.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Lỗi: " + ex.Message;
        }

        return response;
    }
    public async Task<CreateProductDetailResponse> AddProductDetailsAsync(AddProductDetailDTO dto)
    {
        var response = new CreateProductDetailResponse();
        try
        {
            var newDetails = new List<ProductDetail>();

            foreach (var item in dto.Details)
            {
                await ValidateProductDetailReferencesAsync(item.SizeId, item.ColorId, item.FabricId);

                var detail = new ProductDetail
                {
                    ProductId = dto.ProductId,
                    SizeId = item.SizeId,
                    ColorId = item.ColorId,
                    FabricId = item.FabricId
                };

                newDetails.Add(detail);
            }

            _context.ProductDetails.AddRange(newDetails);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = $"Đã thêm {newDetails.Count} chi tiết sản phẩm thành công.";
            // response.Data = _mapper.Map... nếu bạn cần trả về chi tiết nào đã thêm.
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Lỗi: " + ex.Message;
        }

        return response;
    }
    public async Task<CreateImageResponse> AddImagesToDetailAsync(int detailId, List<string> imageUrls)
    {
        var response = new CreateImageResponse();
        try
        {
            foreach (var url in imageUrls)
            {
                var image = new ImagesProduct
                {
                    FullPath = url,
                    ProductDetailId = detailId
                };
                _context.ImagesProducts.Add(image);
                response = _mapper.ToCreateImageResponse(image);
            }

            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Thêm ảnh thành công.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Lỗi: " + ex.Message;
        }

        return response;
    }


    ///
    public async Task DeleteProductDetailAsync(int detailId)
    {
        var detail = await GetDetailOrThrowAsync(detailId);
        // Kiểm tra trước khi xóa
        await ValidateDetailsNotInUseAsync(new[] { detailId });

        _context.ImagesProducts.RemoveRange(detail.Images);
        _context.ProductDetails.Remove(detail);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteProductAsync(int productId)
    {
        var product = await _context.Products
            .Include(p => p.ProductDetails)
                .ThenInclude(d => d.Images)
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        if (product == null)
            throw new Exception($"Sản phẩm với ID={productId} không tồn tại.");

        var detailIds = product.ProductDetails.Select(d => d.DetailId).ToList();
        // Kiểm tra trước khi xóa toàn bộ
        await ValidateDetailsNotInUseAsync(detailIds);

        foreach (var detail in product.ProductDetails)
            _context.ImagesProducts.RemoveRange(detail.Images);

        _context.ProductDetails.RemoveRange(product.ProductDetails);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    //validation
    private async Task ValidateCategoryExistsAsync(int categoryId)
    {
        var exists = await _context.CategoryProducts.AnyAsync(c => c.CategoryId == categoryId);
        if (!exists)
        {
            throw new Exception("Danh mục sản phẩm không hợp lệ!");
        }
    }
    private async Task ValidateProductDetailReferencesAsync(int sizeId, int colorId, int fabricId)
    {
        var sizeExists = await _context.Sizes.AnyAsync(s => s.SizeId == sizeId);
        var colorExists = await _context.Colors.AnyAsync(c => c.ColorId == colorId);
        var fabricExists = await _context.Fabrics.AnyAsync(f => f.FabricId == fabricId);

        if (!sizeExists || !colorExists || !fabricExists)
        {
            throw new Exception("Thông tin chi tiết sản phẩm không hợp lệ (size, màu sắc hoặc chất liệu không tồn tại)!");
        }
    }
    private async Task ValidateImageExistsAsync(int imageId, int productDetailId)
    {
        var exists = await _context.ImagesProducts
            .AnyAsync(i => i.Id == imageId && i.ProductDetailId == productDetailId);
        if (!exists)
        {
            throw new Exception($"Ảnh với ImageId={imageId} không tồn tại hoặc không thuộc về ProductDetailId={productDetailId}.");
        }
    }

    private async Task<ProductDetail> GetDetailOrThrowAsync(int detailId)
    {
        var detail = await _context.ProductDetails
            .Include(d => d.Images)
            .FirstOrDefaultAsync(d => d.DetailId == detailId);

        if (detail == null)
            throw new Exception($"Chi tiết sản phẩm với ID={detailId} không tồn tại.");


        return detail;
    }
    private async Task ValidateDetailsNotInUseAsync(IEnumerable<int> detailIds)
    {
        var idList = detailIds.ToList(); // tránh IEnumerable gây lặp truy vấn

        foreach (var id in idList)
        {
            bool inInput = await _context.InputReceiptDetails.AnyAsync(x => x.ProductDetailId == id);
            bool inOutput = await _context.OutputReceiptDetails.AnyAsync(x => x.ProductDetailId == id);

            if (inInput || inOutput)
            {
                var parts = new List<string>();
                if (inInput) parts.Add("trong phiếu nhập");
                if (inOutput) parts.Add("trong phiếu xuất");
                throw new Exception($"Không thể thực hiện vì chi tiết sản phẩm với ID = {id} đang được sử dụng {string.Join(" và ", parts)}.");
            }
        }
    }

}

