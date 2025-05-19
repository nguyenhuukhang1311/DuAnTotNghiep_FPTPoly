using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.ColorModel;
using MenShop_Assignment.Models.FabricModel;

namespace MenShop_Assignment.Mapper
{
    public class FabricMapper
    {
        private readonly ApplicationDbContext _context;

        public FabricMapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public FabricViewModel ToGetFabric(Fabric fabric)
        {
            return new FabricViewModel
            {
                FabricId = fabric.FabricId,
                Name = fabric.Name
            };
        }
    }
}
