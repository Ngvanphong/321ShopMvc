using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Data.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public  ProductCategoryViewModel ProductCategory { set; get; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Comment { get; set; }

        public string Model { get; set; }

        public string MadeIn { get; set; }

        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

        public Status Status { set; get; }
        public string ProductStatus { get; set; }



    }
}
