using MenShop_Assignment.Datas;
using MenShop_Assignment.DTOs;
using MenShop_Assignment.Models;

namespace MenShop_Assignment.Repositories.BranchesRepositories
{
    public interface IBranchRepository
    {
        Task<List<BranchViewModel>?> GetBranchesAsync();
        Task<BranchViewModel?> GetBranchByIdAsync(int Id);
        Task<Branch?> CreateBranchAsync(CreateUpdateBranchDTO branchDTO);
        Task<Branch?> UpdateBranchAsync(CreateUpdateBranchDTO branchDTO);
		Task<List<ProductViewModel>?> GetBranchProductsAsync(int branchId);
		Task<List<ProductDetailViewModel>?> GetDetailProductBranchAsync(int branchId, int productId);
		Task<List<ProductViewModel>> SmartSearchProductsByNameAsync(int branchId, string name);
		Task<List<ProductViewModel>> SmartSearchProductsByIdAsync(int branchId, int productId);
	}
}
