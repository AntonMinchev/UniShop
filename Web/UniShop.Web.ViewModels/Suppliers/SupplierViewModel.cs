using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Suppliers
{
    public class SupplierViewModel : IMapFrom<SupplierServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal PriceToOffice { get; set; }

        public decimal PriceToHome { get; set; }

    }
}
