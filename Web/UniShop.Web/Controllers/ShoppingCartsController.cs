using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.InputModels;
using UniShop.Web.ViewModels.Products;
using UniShop.Web.ViewModels.ShoppingCarts;

namespace UniShop.Web.Controllers
{
    [Authorize]
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
                Product = product.Product.To<ProductViewModel>(),
                Quantity = product.Quantity,
                TotalPrice = product.Quantity * product.Product.Price

            }).ToList();

            return this.View(shoppingCartProductsViewModel);
        }


        //[HttpPost("/ShoppingCarts/Add")]
        public IActionResult Add(int id)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;

            this.shoppingCartsService.AddShoppingCartProduct(id, username);

            return this.RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;


            this.shoppingCartsService.RemoveShoppingCartProduct(id, username);

            return this.RedirectToAction("Index");
        }

        public IActionResult Increase(int id)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;

            var isUncrease =  this.shoppingCartsService.IncreaseQuantity(id, username);

            if (!isUncrease)
            {
                this.TempData["message"] = "Не може да поръчате от наличното количество за даден продукт!!! ";
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Index");
        }
        

          public IActionResult Reduce(int id)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;

           var isReduce = this.shoppingCartsService.ReduceQuantity(id, username);

            if (!isReduce)
            {
                this.TempData["message"] = "Не може да поръчате по-малко от 1 продукт,ако искате да не го поръчвате го премахнете от количката!!! ";
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Index");
        }
    }
}