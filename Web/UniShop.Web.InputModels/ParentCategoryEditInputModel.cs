using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Web.InputModels.Common;

namespace UniShop.Web.InputModels
{
    public class ParentCategoryEditInputModel
    {
        public int Id { get; set; }

        [Display(Name = InputModelsConstants.Name)]
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [StringLength(InputModelsConstants.ParentCategoryMaxLength, MinimumLength = InputModelsConstants.ParentCategoryMinLength, ErrorMessage = InputModelsConstants.StringLengthErrorMessage)]
        public string Name { get; set; }
    }
}
