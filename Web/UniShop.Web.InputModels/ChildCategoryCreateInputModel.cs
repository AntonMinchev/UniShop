using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniShop.Web.InputModels
{
    public class ChildCategoryCreateInputModel
    {
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1} символа.")]
        [Display(Name = "Име")]
        public string Name { get; set; }


        [Required]
        public int ParentCategoryId { get; set; }

    }
}
