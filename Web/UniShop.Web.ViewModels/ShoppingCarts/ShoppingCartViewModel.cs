using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Products;

namespace UniShop.Web.ViewModels.ShoppingCarts
{
    public class ShoppingCartViewModel : IMapFrom<ShoppingCartProductServiceModel>
    {
       
        public ProductViewModel Product { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        
    }
}
