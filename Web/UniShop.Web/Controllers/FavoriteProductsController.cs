using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Web.ViewModels.FavoriteProducts;
using X.PagedList;

namespace UniShop.Web.Controllers
{
    [Authorize]
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

            return this.RedirectToAction("All");
        }

        public IActionResult All(int? pages)
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

            int pageNumber = pages ?? 1;

            var pageFavoriteProducts = favoriteProductsViewModels.ToPagedList(pageNumber, 3);

            return this.View(pageFavoriteProducts);
        }


        public IActionResult Remove(int id)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;

            this.favoriteProductsService.RemoveFavoriteProduct(id, username);

            return this.RedirectToAction("All");
        }

    }
}