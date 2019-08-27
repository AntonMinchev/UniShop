using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Web.InputModels.Common;

namespace UniShop.Web.InputModels
{
    public class ProductEditInputModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = InputModelsConstants.Name)]
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [StringLength(InputModelsConstants.ProductMaxLength, MinimumLength = InputModelsConstants.ProductMinLength, ErrorMessage = InputModelsConstants.StringLengthErrorMessage)]
        public string Name { get; set; }

        [Display(Name = InputModelsConstants.Price)]
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [Range(typeof(decimal), InputModelsConstants.PriceMinValue, InputModelsConstants.PriceMaxValue, ErrorMessage = InputModelsConstants.PriceErrorMessage)]
        public decimal Price { get; set; }

        [Display(Name = InputModelsConstants.Quantity)]
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [Range(InputModelsConstants.QuantityMinNumber, InputModelsConstants.QuantityMaxNumber, ErrorMessage = InputModelsConstants.QuantityErrorMessage)]
        public int Quantity { get; set; }

        [Display(Name = InputModelsConstants.Description)]
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [StringLength(InputModelsConstants.DescriptionMaxLength, MinimumLength = InputModelsConstants.DescriptionMinLength, ErrorMessage = InputModelsConstants.StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = InputModelsConstants.Specification)]
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [StringLength(InputModelsConstants.SpecificationMaxLength, MinimumLength = InputModelsConstants.SpecificationMinLength, ErrorMessage = InputModelsConstants.StringLengthErrorMessage)]
        public string Specification { get; set; }

        [Display(Name = InputModelsConstants.Image)]
        public IFormFile Image { get; set; }

        [Display(Name = InputModelsConstants.ChildCategoryName)]
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        public int ChildCategoryId { get; set; }
    }
}
