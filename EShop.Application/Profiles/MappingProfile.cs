using AutoMapper;
using EShop.Application.Dtos;
using EShop.Domain.Dtos.ProductAgg;
using EShop.Domain.Dtos.UserAgg;
using EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, AddProductDto>().ReverseMap();
            CreateMap<Product, DeleteProductDto>().ReverseMap();
            CreateMap<ProductDetailsDto, DeleteProductDto>().ReverseMap();

            CreateMap<Product, ProductDetailsDto>()
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(src => src.Category.Title));
            
            CreateMap<Product, ShowProductDto>()
                .ForMember(d=>d.CategoryName,opt=>opt.MapFrom(src=>src.Category.Title));

            CreateMap<Product, UpdateProductDto>().ReverseMap();


            CreateMap<User,CreateUserByAdminDto>().ReverseMap();
            CreateMap<User,DeleteUserByAdminDto>().ReverseMap();
            CreateMap<User,GetUserDto>().ReverseMap();
            CreateMap<User,LoginUserDto>().ReverseMap();
            CreateMap<User,RegisterUserDto>().ReverseMap();
            CreateMap<User,UpdatePasswordDto>().ReverseMap();
            CreateMap<User,UpdateUserByAdminDto>().ReverseMap();
            CreateMap<User,UserInfoForAdminDto>().ReverseMap();

            CreateMap<UserCartItem, CartItemDto>()
    .ForMember(d => d.ProductId, opt => opt.MapFrom(src => src.ProductId))
    .ForMember(d => d.Title, opt => opt.MapFrom(src => src.Product.Title))
    .ForMember(d => d.UnitPrice, opt => opt.MapFrom(src => src.Product.UnitCost))
    .ForMember(d => d.Quantity, opt => opt.MapFrom(src => src.Count));
        }
    }
}
