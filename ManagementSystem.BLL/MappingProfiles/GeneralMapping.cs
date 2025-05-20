using AutoMapper;
using ManagementSystem.BLL.DTO.AppRoleDtos;
using ManagementSystem.BLL.DTO.AppUserDtos;
using ManagementSystem.BLL.DTO.CategoryDtos;
using ManagementSystem.BLL.DTO.ProductDtos;
using ManagementSystem.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.MappingProfiles
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Product, ProductResponseDto>().ReverseMap();
            CreateMap<Product, CreateProductRequestDto>().ReverseMap();
            CreateMap<Product, UpdateProductRequestDto>().ReverseMap();

            CreateMap<Category, CategoryResponseDto>().ReverseMap();
            CreateMap<Category, CreateCategoryRequestDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryRequestDto>().ReverseMap();

            CreateMap<AppUser, AppUserResponseDto>().ReverseMap();
            CreateMap<AppUser, CreateAppUserRequestDto>().ReverseMap();
            CreateMap<AppUser, UpdateAppUserRequestDto>().ReverseMap();

            CreateMap<AppRole, AppRoleResponseDto>().ReverseMap();
            CreateMap<AppUser, CreateAppRoleRequestDto>().ReverseMap();
            CreateMap<AppUser, UpdateAppRoleRequestDto>().ReverseMap();
        }
    }
}
