using AutoMapper;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.business.Concrete.Mapper
{
    internal class DTOMapper:Profile
    {
        public DTOMapper() 
        {
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<CartDTO, Cart>().ReverseMap();
            CreateMap<CartItemDTO, CartItem>().ReverseMap();
        }
    }
}
