using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Data.ViewModels.Contact
{
    public class ContactViewModel
    {      
        public string Id { get; set; }

        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [StringLength(50)]
        public string Phone { set; get; }

        [StringLength(250)]
        public string Email { set; get; }

        [StringLength(250)]
        public string Address { set; get; }

        [StringLength(250)]
        public string Bank1 { set; get; }

        [StringLength(250)]
        public string Bank2 { set; get; }

        [StringLength(250)]
        public string Bank3 { set; get; }

        public Status Status { set; get; }
    }
}
