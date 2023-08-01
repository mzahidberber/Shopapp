using AutoMapper;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using shopapp.web.Models.Entity;

namespace shopapp.web.Mapper
{
    internal class ModelMapper:Profile
    {
        public ModelMapper()
        {
            CreateMap<ProductModel, ProductDTO>().ReverseMap();
            CreateMap<CategoryModel, CategoryDTO>().ReverseMap();
            CreateMap<ProductCategoryModel, ProductCategoryDTO>().ReverseMap();
            CreateMap<UserRoleModel, UserRole>().ReverseMap();
            CreateMap<OrderModel, OrderDTO>().ReverseMap();
            CreateMap<UserModel, UserDTO>().ReverseMap();
        }
    }
}
