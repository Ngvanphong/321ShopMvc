﻿using System.ComponentModel.DataAnnotations;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Data.ViewModels
{
    public class PageViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [MaxLength(256)]
        [Required]
        public string Alias { set; get; }

        public string Content { set; get; }

        public Status Status { set; get; }
    }
}