using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.SizeModel;

namespace MenShop_Assignment.Repositories
{
    public interface ISizeRespository
    {
        Task<List<SizeViewModel>> GetAllSize();
        Task<SizeViewModel> GetByIdSize(int Id);
        Task<Size> CreateSize(Size size);
        Task<Size> UpdateSizeAsync(int Id, Size size);
        Task<bool> DeleteSize(int Id);
    }
}
