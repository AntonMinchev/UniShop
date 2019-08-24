using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;

namespace UniShop.Web.InputModels
{
    public class ParentCategoryCreateInputModel
    {
        [Display(Name = "Име")]
        [Required(ErrorMessage ="Полето е задължително!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1} символа.")]
        public string Name { get; set; }
    }
}
