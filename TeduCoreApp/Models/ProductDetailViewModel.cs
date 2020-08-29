using System.Collections.Generic;
using TeduCoreApp.Data.ViewModels.Product;

namespace TeduCoreApp.Models
{
    public class ProductDetailViewModel
    {
        public ProductViewModel ProductDetail { get; set; }

        public List<ProductViewModel> ProductRelate { get; set; }

        public List<ProductViewModel> ProductUpsell { get; set; }
    }
}