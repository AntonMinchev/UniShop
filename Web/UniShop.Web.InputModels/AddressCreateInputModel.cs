using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;

namespace UniShop.Web.InputModels
{
    public class AddressCreateInputModel 
    {
        public string City { get; set; }
        
        public string Street { get; set; }

        public string BuildingNumber { get; set; }
    }
}
