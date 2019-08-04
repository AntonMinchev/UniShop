using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UniShop.Services;
using UniShop.Services.Mapping;
using UniShop.Web.ViewModels;

namespace UniShop.Web.Components
{
    public class ShoppingCartComponent : ViewComponent
    {
        private IShoppingCartsService shoppingCartService;

        public ShoppingCartComponent(IShoppingCartsService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        public IViewComponentResult Invoke()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.View(new List<ShoppingCartProductViewModel>());
            }
            var shoppingCartProducts = this.shoppingCartService.GetAllShoppingCartProducts(this.User.Identity.Name).ToList();


            var shoppingCartProductsViewModel = shoppingCartProducts.Select(product => new ShoppingCartProductViewModel
            {
                Id = product.ProductId,
                Product=product.Product,
                Quantity = product.Quantity,
                TotalPrice = product.Quantity * product.Product.Price

            }).ToList();

            if (shoppingCartProductsViewModel.Count == 0)
            {
                return this.View(new List<ShoppingCartProductViewModel>());
            }

            return this.View(shoppingCartProductsViewModel);
        }
    }
}

