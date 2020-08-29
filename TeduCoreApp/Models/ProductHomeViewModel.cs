using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.ViewModels.Product;

namespace TeduCoreApp.Models
{
    public class ProductHomeViewModel
    {
        public ProductHomeViewModel()
        {
            ListProductCategorySub = new List<ProductCategorySub>();
        }
       public ProductCategoryViewModel CategoryParent { set; get; }
       public  List<ProductCategorySub> ListProductCategorySub { set; get; }
    }
}
