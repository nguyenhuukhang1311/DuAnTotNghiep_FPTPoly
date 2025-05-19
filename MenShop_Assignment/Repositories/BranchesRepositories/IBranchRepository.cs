using MenShop_Assignment.DTO;

namespace MenShop_Assignment.Repositories.BranchesRepositories
{
    public interface IBranchRepository
    {
        Task<IEnumerable<BranchDto>> GetBranchesAsync();
        Task<BranchDto?> GetBranchByIdAsync(int id);
        Task<BranchDto?> CreateBranchAsync(BranchCreateDto dto);
        Task<bool> UpdateBranchAsync(int id, BranchUpdateDto dto);
    }
}
