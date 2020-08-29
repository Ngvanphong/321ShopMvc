using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Products")]
    public class Product : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public Product()
        {

        }
        public Product(ProductViewModel productVm)
        {
            Name = productVm.Name;
            CategoryId = productVm.CategoryId;         
            Status = productVm.Status;
            Price = productVm.Price;
            Comment = productVm.Comment;
            Model = productVm.Model;
            MadeIn = productVm.MadeIn;
            ProductStatus = productVm.ProductStatus;
        }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual ProductCategory ProductCategory { get; set; }

        [Required]
        [MaxLength(50)]
        public string Price { get; set; }

        [MaxLength(250)]
        public string Comment { get; set; }

        [MaxLength(50)]
        public string Model { get; set; }

        [MaxLength(50)]
        public string MadeIn { get; set; }

        [MaxLength(50)]
        public string ProductStatus { get; set; }

        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

        public Status Status { set; get; }
        
        
    }
}
