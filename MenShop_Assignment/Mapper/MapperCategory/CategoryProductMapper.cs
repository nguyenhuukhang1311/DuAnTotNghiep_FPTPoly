using MenShop_Assignment.Datas;
using MenShop_Assignment.Models;
using MenShop_Assignment.Models.CategoryModels;
using AutoMapper;

namespace MenShop_Assignment.Mapper.MapperCategory
{
    public class CategoryProductMapper
    {
        public CategoryModelView ToCategoryProductView(CategoryProduct categoryProduct)
        {
            return new CategoryModelView
            {
                CategoryId = categoryProduct.CategoryId,
                Name = categoryProduct.Name
            };
        }
    }
}
