using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniShop.Web.InputModels
{
    public class ParentCategoryEditInputModel
    {
        [Display(Name = "Име")]
        [Required(ErrorMessage = "Полето е задължително!")]
        public string Name { get; set; }
    }
}
