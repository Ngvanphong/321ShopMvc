using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Models;
using static TeduCoreApp.Utilities.Constants.CommonConstants;

namespace TeduCoreApp.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        private ISlideService _slideService;
        private IConfiguration _config;
        private ISystemConfigService _systemConfig;
        private IProductCategoryService _productCategoryService;
        private IContactService _contactService;
        public HomeController(IProductService productService, IProductCategoryService productCategoryService,
            ISlideService slideService, IConfiguration config, ISystemConfigService systemConfig,
            IContactService contactService)
        {
            _productService = productService;
            _slideService = slideService;
            _config = config;
            _systemConfig = systemConfig;
            _productCategoryService = productCategoryService;
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            
            HomeViewModel homeVm = new HomeViewModel() { };
            homeVm.ListSlide = _slideService.GetAll();
            homeVm.ListCategoryHomeFlag = _productCategoryService.GetAlllParentWithHomeFlag(true);
            homeVm.ListCategoryNotHomeFlag = _productCategoryService.GetAlllParentWithHomeFlag(false);
            homeVm.Contact = _contactService.GetContact();

            List<ProductHomeViewModel> listProductHomeVm = new List<ProductHomeViewModel>();

            List<ProductCategoryViewModel> listParent = _productCategoryService.GetAllParent();
            
            foreach(ProductCategoryViewModel parent in listParent)
            {
                ProductHomeViewModel productHomeVm = new ProductHomeViewModel();
                productHomeVm.CategoryParent = parent;
                List<ProductCategoryViewModel> listCategoryHasProduct = _productCategoryService.GetAllByParentId(parent.Id);

                List<ProductCategorySub> listProductSub = new List<ProductCategorySub>();
                foreach(var category in listCategoryHasProduct)
                {
                    ProductCategorySub productSub = new ProductCategorySub();
                    productSub.Category = category;
                    productSub.ListProduct = _productService.GetAllByCategory(category.Id);
                    listProductSub.Add(productSub);

                }
                productHomeVm.ListProductCategorySub = listProductSub;
                listProductHomeVm.Add(productHomeVm);
            }

            homeVm.ListProductHome = listProductHomeVm;
            ViewBag.HomeTitle = _systemConfig.Detail("HomeTitle").Value1;
            ViewBag.HomeMetaDescription = _systemConfig.Detail("HomeMetaDescription").Value1;
            ViewBag.HomeMetaKeyword = _systemConfig.Detail("HomeMetaKeyword").Value1;

            return View(homeVm);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}