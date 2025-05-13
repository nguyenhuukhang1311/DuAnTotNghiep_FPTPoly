using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.ColorModel;
using MenShop_Assignment.Models.FabricModel;

namespace MenShop_Assignment.Repositories
{
    public interface IFabricRepository
    {
        Task<List<FabricViewModel>> GetAllFabric();
        Task<FabricViewModel> GetByIdFabric(int Id);
        Task<Fabric> CreateFabric(Fabric fabric);
        Task<Fabric> GetById(int Id);
        Task<Fabric> UpdateFabric(int Id, Fabric fabric);
        Task<Fabric> DeleteFabric(int Id);
    }
}
