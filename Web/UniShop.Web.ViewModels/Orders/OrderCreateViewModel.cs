using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Addresses;
using UniShop.Web.ViewModels.Common;
using UniShop.Web.ViewModels.Products;
using UniShop.Web.ViewModels.ShoppingCarts;
using UniShop.Web.ViewModels.Suppliers;

namespace UniShop.Web.ViewModels.Orders
{
    public class OrderCreateViewModel
    {
        [Display(Name = ViewModelsConstants.Name)]
        public string FullName { get; set; }

        [Display(Name = ViewModelsConstants.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public ICollection<AddressViewModel> Addresses { get; set; }

        public ICollection<SupplierViewModel> Suppliers { get; set; }

        public ICollection<ShoppingCartProductViewModel> Products { get; set; }

    }
}
