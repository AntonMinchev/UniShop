using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop;
using UniShop.Web.InputModels;
using UniShop.Data.Models;

namespace UniShop.Services.Models
{
    public class SupplierServiceModel : IMapFrom<SupplierCreateInputModel> ,IMapFrom<Supplier>,IMapFrom<SupplierEditInputModel>,IMapTo<SupplierEditInputModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal PriceToOffice { get; set; }

        public decimal PriceToHome { get; set; }
    }
}
