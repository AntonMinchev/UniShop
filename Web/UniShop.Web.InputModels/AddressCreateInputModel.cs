using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Web.InputModels.Common;

namespace UniShop.Web.InputModels
{
    public class AddressCreateInputModel 
    {
        [Display(Name = InputModelsConstants.City)]
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [StringLength(InputModelsConstants.CityMaxLength, MinimumLength = InputModelsConstants.CityMinLength, ErrorMessage = InputModelsConstants.StringLengthErrorMessage)]
        public string City { get; set; }

        [Display(Name = InputModelsConstants.Street)]
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [StringLength(InputModelsConstants.StreetMaxLength, MinimumLength = InputModelsConstants.StreetMinLength, ErrorMessage = InputModelsConstants.StringLengthErrorMessage)]
        public string Street { get; set; }

        [Display(Name = InputModelsConstants.BuildingNumber)]
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [StringLength(InputModelsConstants.BuildingNumberMaxNumber, MinimumLength = InputModelsConstants.BuildingNumberMinNumber, ErrorMessage = InputModelsConstants.StringLengthErrorMessage)]
        public string BuildingNumber { get; set; }
    }
}
