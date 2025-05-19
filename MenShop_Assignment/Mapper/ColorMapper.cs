using System.Drawing;
using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.ColorModel;

namespace MenShop_Assignment.Mapper
{
    public class ColorMapper
    {
        private readonly ApplicationDbContext _context;

        public ColorMapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public ColorViewModel ToGetColor(Datas.Color color)
        {
            return new ColorViewModel
            {
                ColorId = color.ColorId,
                Name = color.Name
            };
        }
    }
}
