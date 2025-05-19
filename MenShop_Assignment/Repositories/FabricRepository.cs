using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models.ColorModel;
using MenShop_Assignment.Models.FabricModel;
using Microsoft.EntityFrameworkCore;

namespace MenShop_Assignment.Repositories
{
    public class FabricRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly FabricMapper _mapper;

        public FabricRepository(ApplicationDbContext context, FabricMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FabricViewModel>> GetAllFabric()
        {
            return await _context.Fabrics.Select(x=>_mapper.ToGetFabric(x)).ToListAsync();
        }

        public async Task<FabricViewModel> GetByIdFabric(int Id)
        {
            var Respon = await _context.Fabrics.Where(x => x.FabricId == Id).FirstOrDefaultAsync();
            return _mapper.ToGetFabric(Respon);
        }

        public async Task<Fabric> CreateFabric(Fabric fabric)
        {
            _context.Fabrics.Add(fabric);
            await _context.SaveChangesAsync();
            return fabric;
        }

        public async Task<Fabric> GetById(int Id)
        {
            return await _context.Fabrics.FindAsync(Id);
        }

        public async Task<Fabric> UpdateFabric(int Id, Fabric fabric)
        {
            var check = await _context.Fabrics.FindAsync(Id);
            if (check != null)
            {
                check.Name = fabric.Name;
                await _context.SaveChangesAsync();
                return check;
            }
            return null;
        }

        public async Task<bool> DeleteFabric(int Id)
        {
            var Check = await _context.Fabrics.FindAsync(Id);
            if (Check == null)
            {
                return false;
            }

            _context.Fabrics.Remove(Check);
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
