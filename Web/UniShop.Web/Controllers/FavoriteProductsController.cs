using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Web.ViewModels.FavoriteProducts;

namespace UniShop.Web.Controllers
{
    public class FavoriteProductsController : BaseController
    {
        private readonly IFavoriteProductsService favoriteProductsService;

        public FavoriteProductsController(IFavoriteProductsService favoriteProductsService)
        {
            this.favoriteProductsService = favoriteProductsService;
        }

        public IActionResult Add(int id)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;

            this.favoriteProductsService.AddFavoriteProduct(id, username);

            return this.Redirect("/");
        }

        public IActionResult All()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var favoriteProducts = this.favoriteProductsService.GetAllFavoriteProductsByUserId(userId).ToList();

            var favoriteProductsViewModels = favoriteProducts.Select(p => new UniShopUserFavoriteProductViewModel
            {
                ProductId = p.ProductId,
                ProductImage = p.Product.Image,
                ProductName = p.Product.Name,
                ProductPrice = p.Product.Price
            });

            return this.View(favoriteProductsViewModels);
        }
    }
}