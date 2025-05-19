using AutoMapper;
using MenShop_Assignment.Datas;
using MenShop_Assignment.Models;
using MenShop_Assignment.Models.CategoryModels;

namespace MenShop_Assignment.Mapper.MapperCategory
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryModelView, CategoryProduct>()
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore()); // Bỏ qua gán ID

            CreateMap<CategoryProduct, CategoryModelView>(); 
        }
    }

}
