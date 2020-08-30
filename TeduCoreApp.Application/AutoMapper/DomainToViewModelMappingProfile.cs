using AutoMapper;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels;
using TeduCoreApp.Data.ViewModels.Contact;
using TeduCoreApp.Data.ViewModels.FunctionVm;
using TeduCoreApp.Data.ViewModels.Identity;

using TeduCoreApp.Data.ViewModels.Permission;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Data.ViewModels.Slide;
using TeduCoreApp.Data.ViewModels.SystemConfig;

namespace TeduCoreApp.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<Function, FunctionViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<AppRole, AppRoleViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<Permission, PermissionViewModel>();
            CreateMap<Slide, SlideViewModel>();
            CreateMap<Contact, ContactViewModel>();
            CreateMap<SystemConfig, SystemConfigViewModel>();
            CreateMap<Page, PageViewModel>();
            CreateMap<PageImage, PageImageViewModel>();
        }
    }
}