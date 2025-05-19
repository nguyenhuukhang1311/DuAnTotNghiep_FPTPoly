using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.ColorModel;

namespace MenShop_Assignment.Repositories
{
    public interface IColorRepository
    {
        Task<List<ColorViewModel>> GetAllColor();
        Task<ColorViewModel> GetByIdColor(int Id);
        Task<Color> CreateColor(Color color);
        Task<Color> GetById(int Id);
        Task<Color> UpdateColor(int Id, Color color);
        Task<Color> DeleteColor(int Id);
    }
}
