using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using TeduCoreApp.Data.ViewModels.Contact;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Data.ViewModels.Slide;

namespace TeduCoreApp.Models
{
    public class HomeViewModel
    {
        public List<SlideViewModel> ListSlide { get; set; }

        public string DomainApi { get; set; }

        public List<ProductCategoryViewModel> ListCategoryHomeFlag { set; get; }

        public List<ProductCategoryViewModel> ListCategoryNotHomeFlag { set; get; }

        public ContactViewModel Contact { set; get; }

        public List<ProductHomeViewModel> ListProductHome { set; get; }

        
    }
}