using MenShop_Assignment.Datas;
using MenShop_Assignment.DTO;
using MenShop_Assignment.Models.Branch;
using MenShop_Assignment.Repositories.BranchesRepositories;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;

        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BranchDto>> GetBranchesAsync()
        {
            return await _context.Branches
                .Select(b => new BranchDto
                {
                    BranchId = b.BranchId,
                    Address = b.Address,
                    ManagerId = b.ManagerId
                })
                .ToListAsync();
        }

        public async Task<BranchDto?> GetBranchByIdAsync(int id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null) return null;

            return new BranchDto
            {
                BranchId = branch.BranchId,
                Address = branch.Address,
                ManagerId = branch.ManagerId
            };
        }

        public async Task<BranchDto?> CreateBranchAsync(BranchCreateDto dto)
        {
            var inputAddress = dto.Address?.Trim();
            if (!AddressValidator.IsValidVietnamAddress(inputAddress))
                return null;
            var branch = new Branch { Address = inputAddress };
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();

            return new BranchDto
            {
                BranchId = branch.BranchId,
                Address = branch.Address,
                ManagerId = branch.ManagerId
            };
        }

        public async Task<bool> UpdateBranchAsync(int id, BranchUpdateDto dto)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null) return false;

            var inputAddress = dto.Address?.Trim();
            if (!AddressValidator.IsValidVietnamAddress(inputAddress))
                return false;

            branch.Address = inputAddress;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
