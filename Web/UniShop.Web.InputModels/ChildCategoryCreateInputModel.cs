using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Web.InputModels.Common;

namespace UniShop.Web.InputModels
{
    public class ChildCategoryCreateInputModel
    {
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [StringLength(InputModelsConstants.ChildCategoryMaxLength, MinimumLength = InputModelsConstants.ChildCategoryMinLength, ErrorMessage = InputModelsConstants.StringLengthErrorMessage)]
        [Display(Name = InputModelsConstants.Name)]
        public string Name { get; set; }


        [Required]
        public int ParentCategoryId { get; set; }

    }
}
