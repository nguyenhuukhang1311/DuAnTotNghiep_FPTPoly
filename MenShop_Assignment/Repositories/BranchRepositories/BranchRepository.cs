using MenShop_Assignment.Datas;
using MenShop_Assignment.DTOs;
using MenShop_Assignment.Extensions;
using MenShop_Assignment.Models;
using MenShop_Assignment.Repositories.BranchesRepositories;
using Microsoft.EntityFrameworkCore;
using MenShop_Assignment.Mapper;
namespace MenShop_Assignment.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;

        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BranchViewModel>?> GetBranchesAsync()
        {
			var branches = await _context.Branches.Include(b=>b.Manager).ToListAsync();
            if (branches.Any())
			    return branches.Select(BranchMapper.ToBranchViewModel).ToList();
            return null;
		}
        public async Task<BranchViewModel?> GetBranchByIdAsync(int id)
        {
            var branch = await _context.Branches.Where(x=>x.BranchId == id).FirstOrDefaultAsync();
            if (branch == null) 
                return null;
            return BranchMapper.ToBranchViewModel(branch);
        }
        public async Task<Branch?> CreateBranchAsync(CreateUpdateBranchDTO branchDTO)
        {
            if(branchDTO == null||branchDTO.Address==null)
                return null;
            var branch = BranchMapper.ToBranch(branchDTO);
			await _context.Branches.AddAsync(branch);
			await _context.SaveChangesAsync();
			return branch;
        }
        public async Task<Branch?> UpdateBranchAsync(CreateUpdateBranchDTO branchDTO)
        {
			if (branchDTO == null || branchDTO.Address == null)
				return null;
			var branch = await _context.Branches.Where(x => x.BranchId == branchDTO.Id).FirstOrDefaultAsync();
            if (branch == null)
                return null;
			branch.Address = branchDTO.Address;
            await _context.SaveChangesAsync();
            return branch;
		}
		public async Task<List<ProductViewModel>?> GetBranchProductsAsync(int branchId)
		{
            if (branchId == 0)
                return null;
			var products = await _context.BranchDetails
				.Include(x => x.ProductDetail)
					.ThenInclude(pd => pd.Product)
				.Where(x => x.BranchId == branchId)
				.Select(x => x.ProductDetail.Product)
				.Distinct()
				.ToListAsync();
            if (!products.Any()) 
                return null;
			return products.Select(ProductMapper.ToProductViewModel).ToList();
		}
		public async Task<List<ProductDetailViewModel>?> GetDetailProductBranchAsync(int branchId, int productId)
		{
			if (branchId == 0 || productId == 0)
				return null;
			var branchDetails = await _context.BranchDetails
				.Include(x => x.ProductDetail)
					.ThenInclude(pd => pd.Product)
				.Include(x => x.ProductDetail.Color)
				.Include(x => x.ProductDetail.Size)
				.Include(x => x.ProductDetail.Fabric)
				.Include(x => x.ProductDetail.Images)
				.Where(x => x.BranchId == branchId && x.ProductDetail.Product.ProductId == productId)
				.ToListAsync();
			if (branchDetails.Count == 0) 
				return null;
			return branchDetails.Select(BranchMapper.ToBranchDetailViewModel).ToList();
		}
		public async Task<List<ProductViewModel>> SmartSearchProductsByNameAsync(int branchId, string name)
		{
			var products = await _context.BranchDetails
				.Include(x => x.ProductDetail)
					.ThenInclude(pd => pd.Product)
				.Where(x => x.BranchId == branchId && x.ProductDetail.Product.ProductName.Contains(name))
				.Select(x => x.ProductDetail.Product)
				.GroupBy(p => p.ProductId)
				.Select(g => g.First())
				.ToListAsync();
			return products.Select(ProductMapper.ToProductViewModel).ToList();
		}
		public async Task<List<ProductViewModel>> SmartSearchProductsByIdAsync(int branchId, int productId)
		{
			var products = await _context.BranchDetails
				.Include(x => x.ProductDetail)
					.ThenInclude(pd => pd.Product)
				.Where(x => x.BranchId == branchId && x.ProductDetail.Product.ProductId == productId)
				.Select(x => x.ProductDetail.Product)
				.GroupBy(p => p.ProductId)
				.Select(g => g.First())
				.ToListAsync();
			return products.Select(ProductMapper.ToProductViewModel).ToList();
		}
	}
}
