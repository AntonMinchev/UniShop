using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Addresses;
using UniShop.Web.ViewModels.Products;
using UniShop.Web.ViewModels.ShoppingCarts;
using UniShop.Web.ViewModels.Suppliers;

namespace UniShop.Web.ViewModels.Orders
{
    public class OrderCreateViewModel
    {
        [Display(Name = "Име")]
        public string FullName { get; set; }

        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }

        public ICollection<AddressViewModel> Addresses { get; set; }

        public ICollection<SupplierViewModel> Suppliers { get; set; }

        public ICollection<ShoppingCartProductViewModel> Products { get; set; }

    }
}
