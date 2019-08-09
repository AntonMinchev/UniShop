using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Web.InputModels;
using UniShop.Web.ViewModels.ShoppingCarts;

namespace UniShop.Web.Controllers
{
    public class ShoppingCartsController : BaseController
    {
        private readonly IShoppingCartsService shoppingCartsService;

        public ShoppingCartsController(IShoppingCartsService shoppingCartsService)
        {
            this.shoppingCartsService = shoppingCartsService;
        }


        public IActionResult Index()
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;

           var shoppingCartProducts = this.shoppingCartsService.GetAllShoppingCartProducts(username).ToList();

            var shoppingCartProductsViewModel = shoppingCartProducts.Select(product => new ShoppingCartViewModel
            { 
                Product = product.Product,
                Quantity = product.Quantity,
                TotalPrice = product.Quantity * product.Product.Price

            }).ToList();

            return this.View(shoppingCartProductsViewModel);
        }


        //[HttpPost("/ShoppingCarts/Add")]
        public IActionResult Add(ShoppingCartAddInputModel shoppingCartAddInputModel)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;


            this.shoppingCartsService.AddShoppingCartProduct(shoppingCartAddInputModel.Id, username);

            return this.Redirect("/ShoppingCarts/Index");
        }

        public IActionResult Remove(ShoppingCartAddInputModel shoppingCartAddInputModel)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;


            this.shoppingCartsService.RemoveShoppingCartProduct(shoppingCartAddInputModel.Id, username);

            return this.Redirect("/ShoppingCarts/Index");
        }

        public IActionResult Increase(int id)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;

            bool result = this.shoppingCartsService.IncreaseQuantity(id,username);


            return this.Redirect("/ShoppingCarts/Index");
        }
        

          public IActionResult Reduce(int id)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;

            bool result = this.shoppingCartsService.ReduceQuantity(id, username);


            return this.Redirect("/ShoppingCarts/Index");
        }
    }
}