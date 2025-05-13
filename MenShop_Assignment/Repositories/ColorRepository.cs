using System.Drawing;
using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models.ColorModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Color = MenShop_Assignment.Datas.Color;

namespace MenShop_Assignment.Repositories
{
    public class ColorRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ColorMapper _mapper;

        public ColorRepository(ApplicationDbContext context, ColorMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ColorViewModel>> GetAllColor()
        {
            return await _context.Colors.Select(x=>_mapper.ToGetColor(x)).ToListAsync();
        }

        public async Task<ColorViewModel> GetByIdColor(int Id)
        {
            var Respon = await _context.Colors.Where(x=>x.ColorId == Id).FirstOrDefaultAsync();
            return _mapper.ToGetColor(Respon);
        }

        public async Task<Color> CreateColor(Color color)
        {
            _context.Colors.Add(color); 
            await _context.SaveChangesAsync();
            return color;
        }

        public async Task<Color> GetById(int Id)
        {
            return await _context.Colors.FindAsync(Id);
        }

        public async Task<Color> UpdateColor(int Id, Color color)
        {
            var check = await _context.Colors.FindAsync(Id);
            if (check != null)
            {
                check.Name = color.Name;
                await _context.SaveChangesAsync();
                return check;
            }
            return null;
        }

        public async Task<bool> DeleteColor(int Id)
        {
            var Check = await _context.Colors.FindAsync(Id);
            if (Check == null)
            {
                return false;
            }

            _context.Colors.Remove(Check);
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
