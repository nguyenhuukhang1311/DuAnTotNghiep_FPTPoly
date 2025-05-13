using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models.SizeModel;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Repositories
{
    public class SizeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly SizeMapper _sizeMapper;

        public SizeRepository(ApplicationDbContext context, SizeMapper sizeMapper)
        {
            _context = context;
            _sizeMapper = sizeMapper;
        }

        public async Task<List<SizeViewModel>> GetAllSize()
        {
            return await _context.Sizes.Select(x=> _sizeMapper.ToGetSize(x)).ToListAsync();
        }

        public async Task<SizeViewModel> GetByIdSize(int Id)
        {
            var respon = await _context.Sizes.Where(x=>x.SizeId == Id).FirstOrDefaultAsync();
            return _sizeMapper.ToGetSize(respon);
        }

        public async Task<Size> CreateSize(Size size)
        {
            _context.Sizes.Add(size);
            await _context.SaveChangesAsync();
            return size;
        }
        public async Task<Size> GetById(int id)
        {
            return await _context.Sizes.FindAsync(id);
        }
        public async Task<Size> UpdateSizeAsync(int id, Size size)
        {
            var check = await _context.Sizes.FindAsync(id);
            if (check != null)
            {
                check.Name = size.Name;
                await _context.SaveChangesAsync();
                return check;
            }
            return null;
        }

        public async Task<bool> DeleteSize(int Id)
        {
            var Check = await _context.Sizes.FindAsync(Id);
            if (Check == null)
            {
                return false;
            }

            _context.Sizes.Remove(Check);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                // Lỗi khi size bị ràng buộc với bảng khác (FK)
                return false;
            }
        }
    }
}
