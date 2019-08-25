using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Web.InputModels.Common;

namespace UniShop.Web.InputModels
{
    public class ChildCategoryEditInputModel
    {

        public int Id { get; set; }


        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [StringLength(InputModelsConstants.ChildCategoryMaxLength, MinimumLength = InputModelsConstants.ChildCategoryMinLength, ErrorMessage = InputModelsConstants.StringLengthErrorMessage)]
        [Display(Name = InputModelsConstants.Name)]
        public string Name { get; set; }

        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [StringLength(InputModelsConstants.ParentCategoryMaxLength, MinimumLength = InputModelsConstants.ParentCategoryMinLength, ErrorMessage = InputModelsConstants.StringLengthErrorMessage)]
        public string ParentCategoryName { get; set; }

        [Required]
        public int ParentCategoryId { get; set; }
    }
}
