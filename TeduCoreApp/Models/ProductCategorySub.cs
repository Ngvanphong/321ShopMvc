using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Product;

namespace TeduCoreApp.Models
{
    public class ProductCategorySub
    {
        public ProductCategorySub()
        {
            ListProduct = new List<ProductViewModel>();
        }
       public ProductCategoryViewModel Category { set; get; }

       public List<ProductViewModel> ListProduct { set; get; }
    }
}
