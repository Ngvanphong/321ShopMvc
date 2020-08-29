using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.ViewModels.Contact;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ContactDetails")]
    public class Contact : DomainEntity<string>
    {
        public Contact()
        {

        }

        public Contact(ContactViewModel contactVm)
        {
            Name = contactVm.Name;
            Phone = contactVm.Phone;
            Email = contactVm.Email;
            Address = contactVm.Address;
            Status = contactVm.Status;
            Bank1 = contactVm.Bank1;
            Bank2 = contactVm.Bank2;
            Bank3 = contactVm.Bank3;
        }
        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [Required]
        [StringLength(50)]
        public string Phone { set; get; }

        [StringLength(250)]
        public string Email { set; get; }

        [Required]
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
