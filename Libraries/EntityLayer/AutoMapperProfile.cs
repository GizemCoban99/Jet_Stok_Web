using AutoMapper;
using EntityLayer.DTOs;
using EntityLayer.DTOs.JetStokApp;
using EntityLayer.Models;
using EntityLayer.Models.JetStokApp;

namespace EntityLayer
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();

            CreateMap<RoleDTO, Role>();
            CreateMap<Role, RoleDTO>();

            CreateMap<PermissionDTO, Permission>();
            CreateMap<Permission, PermissionDTO>();

            CreateMap<PermissionGroupDTO, PermissionGroup>();
            CreateMap<PermissionGroup, PermissionGroupDTO>();

            CreateMap<GenericTypeDTO, GenericType>();
            CreateMap<GenericType, GenericTypeDTO>();

            CreateMap<GenericValueDTO, GenericValue>();
            CreateMap<GenericValue, GenericValueDTO>();

            CreateMap<MarketplaceCommissionRateDTO, MarketplaceCommissionRate>();
            CreateMap<MarketplaceCommissionRate, MarketplaceCommissionRateDTO>();

            CreateMap<MarketplaceOperationRateDTO, MarketplaceOperationRate>();
            CreateMap<MarketplaceOperationRate, MarketplaceOperationRateDTO>();

            //jetstok create user
            CreateMap<RegisterFormDTO, CompanyDTO>()
             .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.CompanyName))
             .ForMember(dest => dest.phone, opt => opt.MapFrom(src => src.Phone))
             .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email));

            CreateMap<RegisterFormDTO, JetStokUserDTO>()
             .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.surname, opt => opt.MapFrom(src => src.Surname))
             .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.password, opt => opt.MapFrom(src => src.Password))
             .ForMember(dest => dest.phone, opt => opt.MapFrom(src => src.Phone));

            CreateMap<CompanyPackage,Package>();
            CreateMap<Package, CompanyPackage>();

            CreateMap<CompanyPackage, CompanyPackageDTO>();
            CreateMap<CompanyPackageDTO, CompanyPackage>();

            CreateMap<JetStokUserDTO, JetStokUser>();
            CreateMap<JetStokUser, JetStokUserDTO>();


            CreateMap<JetStokCompany, CompanyDTO>();
            CreateMap<CompanyDTO, JetStokCompany>();
        }
    }
}
