using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Web.InputModels.Common;

namespace UniShop.Web.InputModels
{
    public class SupplierEditInputModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = InputModelsConstants.Name)]
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [StringLength(InputModelsConstants.SupplierMaxLength, MinimumLength = InputModelsConstants.SupplierMinLength, ErrorMessage = InputModelsConstants.StringLengthErrorMessage)]
        public string Name { get; set; }

        [Display(Name = InputModelsConstants.PriceToOffice)]
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [Range(typeof(decimal), InputModelsConstants.PriceMinValue, InputModelsConstants.PriceMaxValue, ErrorMessage = InputModelsConstants.PriceErrorMessage)]
        public decimal PriceToOffice { get; set; }

        [Display(Name = InputModelsConstants.PriceToHome)]
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [Range(typeof(decimal), InputModelsConstants.PriceMinValue, InputModelsConstants.PriceMaxValue, ErrorMessage = InputModelsConstants.PriceErrorMessage)]
        public decimal PriceToHome { get; set; }
    }
}
