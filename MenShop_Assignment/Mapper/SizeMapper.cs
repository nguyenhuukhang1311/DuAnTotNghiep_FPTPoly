using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.SizeModel;

namespace MenShop_Assignment.Mapper
{
    public class SizeMapper
    {
        private readonly ApplicationDbContext _context;

        public SizeMapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public SizeViewModel ToGetSize(Size size)
        {
            return new SizeViewModel
            {
                SizeId = size.SizeId,
                Name = size.Name
            };
        }
       
    }
}
