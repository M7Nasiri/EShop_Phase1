using AutoMapper;
using EShop.Application.Dtos;
using EShop.Domain.Dtos.UserAgg;
using EShop.Domain.Entities;

namespace EShop.Web.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetUserDto, UserInfoForAdminDto>()
                .ForMember(d => d.OrdersCount, opt => opt.MapFrom(src => src.Orders.Count));

            CreateMap<User, GetUserOrdersDto>();

   //         CreateMap<User, CreateUserByAdminDto>().ReverseMap();

   //         CreateMap<UserCartItem, CartItemDto>()
   //.ForMember(d => d.ProductId, opt => opt.MapFrom(src => src.ProductId))
   //.ForMember(d => d.Title, opt => opt.MapFrom(src => src.Product.Title))
   //.ForMember(d => d.UnitPrice, opt => opt.MapFrom(src => src.Product.UnitCost))
   //.ForMember(d => d.Quantity, opt => opt.MapFrom(src => src.Count));
        }
    }
}
