using System.Net.WebSockets;
using System.Text.RegularExpressions;
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
        public async Task<List<BranchProductViewModel>> SmartSearchProductsAsync(int branchId, string searchTerm)
        {
            // Kiểm tra xem searchTerm có phải là số nguyên không
            bool isNumber = int.TryParse(searchTerm, out int productId);

            // Kiểm tra xem searchTerm có chứa chỉ ký tự chữ cái và khoảng trắng không
            bool isAlphabetic = Regex.IsMatch(searchTerm, @"^[a-zA-Z\sÀ-ỹ]+$");

            // Nếu searchTerm vừa có số vừa có chữ cái hoặc ký tự đặc biệt
            if (!isNumber && !isAlphabetic)
            {
                throw new ArgumentException("Chuỗi tìm kiếm không hợp lệ. Vui lòng nhập chỉ số hoặc chỉ chữ cái.");
            }

            var query = _context.BranchDetails
                .Where(b => b.BranchId == branchId)
                .Include(b => b.ProductDetail)
                    .ThenInclude(pd => pd.Product)
                .AsQueryable();

            // Tìm theo ProductId nếu searchTerm là số
            if (isNumber)
            {
                query = query.Where(b => b.ProductDetail!.ProductId == productId);
            }
            // Tìm theo ProductName nếu searchTerm là chữ cái
            else
            {
                query = query.Where(b => b.ProductDetail!.Product!.ProductName!.Contains(searchTerm));
            }

            var respon = await query.ToListAsync();
            return respon.Select(x => _mapper.ToDto(x)).ToList();
        }

    }
}
