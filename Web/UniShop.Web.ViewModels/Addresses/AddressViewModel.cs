using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Common;

namespace UniShop.Web.ViewModels.Addresses
{
    public class AddressViewModel : IMapFrom<AddressServiceModel>
    {
        public int Id { get; set; }

        [Display(Name = ViewModelsConstants.City)]
        public string City { get; set; }

        [Display(Name = ViewModelsConstants.Street)]
        public string Street { get; set; }

        [Display(Name = ViewModelsConstants.BuildingNumber)]
        public string BuildingNumber { get; set; }
    }
}
