using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AutoMapper;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Suppliers
{
    public class SupplierViewModel : IMapFrom<SupplierServiceModel>
    {
        [Display(Name = "Номер")]
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Цена до офис")]
        public decimal PriceToOffice { get; set; }

        [Display(Name = "Цена до адрес")]
        public decimal PriceToHome { get; set; }

    }
}
