using AutoMapper;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.business.Concrete.Mapper
{
    internal class DTOMapper : Profile
    {
        public DTOMapper()
        {
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<OrderItemDTO, OrderItem>().ReverseMap();
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<CartDTO, Cart>().ReverseMap();
            CreateMap<CartItemDTO, CartItem>().ReverseMap();
            CreateMap<ImageDTO, Image>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<SubCategoryDTO, SubCategory>().ReverseMap();
            CreateMap<SubCategoryFeatureDTO, SubCategoryFeature>().ReverseMap();
            CreateMap<SubCategoryFeatureValueDTO, SubCategoryFeatureValue>().ReverseMap();
            CreateMap<MainCategoryDTO, MainCategory>().ReverseMap();
            CreateMap<BrandDTO, Brand>().ReverseMap();
            CreateMap<StockDTO, Stock>().ReverseMap();
            CreateMap<StockValueDTO, StockValue>().ReverseMap();
        }
    }
}
