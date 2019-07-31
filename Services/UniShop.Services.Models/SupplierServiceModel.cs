using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop;
using UniShop.Web.InputModels;

namespace UniShop.Services.Models
{
    public class SupplierServiceModel : IMapFrom<SupplierCreateInputModel>
    {
        
        public string Name { get; set; }

        public decimal PriceToOffice { get; set; }

        public decimal PriceToAddress { get; set; }
    }
}
