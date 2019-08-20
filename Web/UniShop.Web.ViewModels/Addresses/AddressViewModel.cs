using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Addresses
{
    public class AddressViewModel : IMapFrom<AddressServiceModel>
    {
        public int Id { get; set; }

        [Display(Name = "Град")]
        public string City { get; set; }

        [Display(Name = "Улица")]
        public string Street { get; set; }

        [Display(Name = "Номер на сградата")]
        public string BuildingNumber { get; set; }
    }
}
