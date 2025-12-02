using AutoMapper;
using EShop.Application.Dtos;
using EShop.Domain.Entities;
using EShop.Domain.ViewModels.ProductAgg;
using EShop.Domain.ViewModels.UserAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, AddProductViewModel>().ReverseMap();
            CreateMap<Product, DeleteProductViewModel>().ReverseMap();

            CreateMap<Product, ProductDetailsViewModel>()
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(src => src.Category.Title));
            
            CreateMap<Product, ShowProductViewModel>()
                .ForMember(d=>d.CategoryName,opt=>opt.MapFrom(src=>src.Category.Title));

            CreateMap<Product, UpdateProductViewModel>().ReverseMap();


            CreateMap<User,CreateUserByAdmin>().ReverseMap();
            CreateMap<User,DeleteUserByAdmin>().ReverseMap();
            CreateMap<User,GetUserViewModel>().ReverseMap();
            CreateMap<User,LoginUserViewModel>().ReverseMap();
            CreateMap<User,RegisterUserViewModel>().ReverseMap();
            CreateMap<User,UpdatePasswordUserViewModel>().ReverseMap();
            CreateMap<User,UpdateUserByAdminViewModel>().ReverseMap();
            CreateMap<User,UserInfoForAdmin>().ReverseMap();

            CreateMap<UserCartItem, CartItemDto>()
    .ForMember(d => d.ProductId, opt => opt.MapFrom(src => src.ProductId))
    .ForMember(d => d.Title, opt => opt.MapFrom(src => src.Product.Title))
    .ForMember(d => d.UnitPrice, opt => opt.MapFrom(src => src.Product.UnitCost))
    .ForMember(d => d.Quantity, opt => opt.MapFrom(src => src.Count));
        }
    }
}
