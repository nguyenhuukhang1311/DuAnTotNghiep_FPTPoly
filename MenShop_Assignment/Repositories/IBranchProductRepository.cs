using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.BranchModel;

namespace MenShop_Assignment.Repositories
{
    public interface IBranchProductRepository
    {
        Task<List<BranchProductViewModel>> GetBranchProductsAsync(int branchId);
        Task<List<BranchProductDetailViewModel>> GetBranchProductDetailAsync(int branchId, int productDetailId);
    }
}
