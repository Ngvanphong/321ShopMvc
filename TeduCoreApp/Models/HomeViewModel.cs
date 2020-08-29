using System.Collections.Generic;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Data.ViewModels.Slide;

namespace TeduCoreApp.Models
{
    public class HomeViewModel
    {
        public List<SlideViewModel> ListSlide { get; set; }

        public List<ProductViewModel> ListNewProduct { get; set; }

        public List<ProductViewModel> ListHotProduct { get; set; }

        public List<ProductViewModel> ListPromotionProduct { get; set; }

        public string DomainApi { get; set; }
    }
}