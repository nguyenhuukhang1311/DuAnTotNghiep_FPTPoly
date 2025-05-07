using Azure;
using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models.BranchModel;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Repositories
{
    public class BranchProductRepository : IBranchProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly BranchMapper _mapper;

        public BranchProductRepository(ApplicationDbContext context, BranchMapper branchMapper)
        {
            _context = context;
            _mapper = branchMapper;

        }

        public async Task<List<BranchProductViewModel>> GetBranchProductsAsync(int branchId)
        {
            var respon = await _context.BranchDetails
                .Where(b => b.BranchId == branchId)
                .Include(b => b.ProductDetail)
                    .ThenInclude(pd => pd.Product)
                .ToListAsync();
            return respon.Select(x => _mapper.ToDto(x)).ToList();
        }

        public async Task<List<BranchProductDetailViewModel>> GetBranchProductDetailAsync(int branchId, int productId)
        {
            var respon = await _context.BranchDetails
                .Include(b => b.ProductDetail)
                    .ThenInclude(pd => pd.Product)
                .Include(b => b.ProductDetail!.Color)
                .Include(b => b.ProductDetail!.Size)
                .Include(b => b.ProductDetail!.Fabric)
                .Where(b => b.BranchId == branchId && b.ProductDetail!.ProductId == productId)
                .ToListAsync();

            return respon.Select(r => _mapper.ToDetailDto(r)).ToList();
        }

    }
}
