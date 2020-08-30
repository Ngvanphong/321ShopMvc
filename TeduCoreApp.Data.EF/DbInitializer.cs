using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public DbInitializer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Top manager"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Staff"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Customer",
                    NormalizedName = "Customer",
                    Description = "Customer"
                });
            }
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "ngvanphong",
                    FullName = "Nguyễn Văn Phong",
                    Email = "ngvanphong2012@gmail.com",
                    Balance = 0,
                }, "vanphong2012");
                var user = await _userManager.FindByNameAsync("ngvanphong");

                await _userManager.AddToRoleAsync(user, "Admin");
            }
            if (_context.Functions.Count() == 0)
            {
                _context.Functions.AddRange(new List<Function>()
                {
                    new Function() {Id = "SYSTEM", Name = "Hệ thống",ParentId = null,SortOrder = 1,Status = Status.Active,URL = "/",IconCss = "fa-desktop"  },
                    new Function() {Id = "ROLE", Name = "Nhóm",ParentId = "SYSTEM",SortOrder = 1,Status = Status.Active,URL = "/main/role/index",IconCss = "fa-home"  },
                    new Function() {Id = "FUNCTION", Name = "Chức năng",ParentId = "SYSTEM",SortOrder = 2,Status = Status.Active,URL = "/main/function/index",IconCss = "fa-home"  },
                    new Function() {Id = "USER", Name = "Người dùng",ParentId = "SYSTEM",SortOrder =3,Status = Status.Active,URL = "/main/user/index",IconCss = "fa-home"  },

                    new Function() {Id = "PRODUCT",Name = "Sản phẩm",ParentId = null,SortOrder = 2,Status = Status.Active,URL = "/",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "PRODUCT_CATEGORY",Name = "Danh mục",ParentId = "PRODUCT",SortOrder =1,Status = Status.Active,URL = "/main/productcategory/index",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "PRODUCT_LIST",Name = "Sản phẩm",ParentId = "PRODUCT",SortOrder = 2,Status = Status.Active,URL = "/main/product/index",IconCss = "fa-chevron-down"  },

                    new Function() {Id = "UTILITY",Name = "Tiện ích",ParentId = null,SortOrder = 4,Status = Status.Active,URL = "/",IconCss = "fa-clone"  },
                    new Function() {Id = "SLIDE",Name = "Slide",ParentId = "UTILITY",SortOrder = 2,Status = Status.Active,URL = "/main/slide/index",IconCss = "fa-clone"  },
                    new Function() {Id = "PAGE",Name = "Trang lẻ",ParentId = "UTILITY",SortOrder = 3,Status = Status.Active,URL = "/main/page/index",IconCss = "fa-clone"  },
                });
                _context.SaveChanges();
            }

            if (_context.Slides.Count() == 0)
            {
                List<Slide> slides = new List<Slide>()
                {
                    new Slide() {Name="Slide 1",Image="/assets/images/slider-1.jpg",Url="#",DisplayOrder = 0,OrtherPageHome = false,Status = true },
                    new Slide() {Name="Slide 2",Image="/assets/images/slider-2.jpg",Url="#",DisplayOrder = 1,OrtherPageHome = false,Status = true },
                    new Slide() {Name="Slide 3",Image="/assets/images/slider-3.jpg",Url="#",DisplayOrder = 2,OrtherPageHome = false,Status = true },
                };
                _context.Slides.AddRange(slides);
                _context.SaveChanges();
            }

            if (_context.Contacts.Count() == 0)
            {

                var contact = new Contact() {Name="Default",Address = "Ho Chi Minh View Nam", Phone = "01223333" };
                _context.Contacts.Add(contact);
                _context.SaveChanges();
            }

            if (_context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory() { Name="Áo nam",HomeFlag=true,Image = "/assets/images/macbook-air-2020.jpg",SeoAlias="ao-nam",ParentId = null,Status=Status.Active,SortOrder=1,
                    },
                     new ProductCategory() { Name="Áo nam 2",HomeFlag=true,Image = "/assets/images/macbook-air-2020.jpg",SeoAlias="ao-nam-2",ParentId = null,Status=Status.Active,SortOrder=2,
                    },
                      new ProductCategory() { Name="Áo nam 3",HomeFlag=true,Image = "/assets/images/macbook-air-2020.jpg",SeoAlias="ao-nam-3",ParentId = null,Status=Status.Active,SortOrder=3,
                    },
                    new ProductCategory() { Name="Áo nam 1.1", HomeFlag=true,SeoAlias="ao-nu",ParentId = null,Status=Status.Active ,SortOrder=2,
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Sản phẩm 1",Price = "100",Status = Status.Active},
                            new Product(){Name = "Sản phẩm 2",Price = "100",Status = Status.Active},
                        }},
                };
                _context.ProductCategories.AddRange(listProductCategory);
                _context.SaveChanges();
            }

            if (!_context.SystemConfigs.Any(x => x.Id == "HomeTitle"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeTitle",
                    Name = "Tiêu đề trang chủ",
                    Value1 = "Trang chủ TeduShop",
                    Status = Status.Active
                });
                _context.SaveChanges();
            }
            if (!_context.SystemConfigs.Any(x => x.Id == "HomeMetaKeyword"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeMetaKeyword",
                    Name = "Từ khoá trang chủ",
                    Value1 = "Trang chủ TeduShop",
                    Status = Status.Active
                });
                _context.SaveChanges();
            }
            if (!_context.SystemConfigs.Any(x => x.Id == "HomeMetaDescription"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeMetaDescription",
                    Name = "Mô tả trang chủ",
                    Value1 = "Trang chủ TeduShop",
                    Status = Status.Active
                });
                _context.SaveChanges();
            }
        }
    }
}