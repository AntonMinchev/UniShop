using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AutoMapper;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Common;

namespace UniShop.Web.ViewModels.Suppliers
{
    public class SupplierViewModel : IMapFrom<SupplierServiceModel>
    {
        [Display(Name = ViewModelsConstants.SupplierId)]
        public int Id { get; set; }

        [Display(Name = ViewModelsConstants.Name)]
        public string Name { get; set; }

        [Display(Name = ViewModelsConstants.PriceToOffice)]
        public decimal PriceToOffice { get; set; }

        [Display(Name = ViewModelsConstants.PriceToHome)]
        public decimal PriceToHome { get; set; }

    }
}
