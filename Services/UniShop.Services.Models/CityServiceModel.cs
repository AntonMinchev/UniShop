using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;

namespace UniShop.Services.Models
{
    public class CityServiceModel : IMapFrom<City>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Postcode { get; set; }

        public ICollection<AddressServiceModel> Addresses { get; set; }
    }
}
