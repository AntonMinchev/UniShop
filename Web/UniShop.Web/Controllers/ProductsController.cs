using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Web.ViewModels;
using UniShop.Web.ViewModels.Products;

namespace UniShop.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet("/Products/Details/{id}")]
        public IActionResult Details(int id)
        {
            ProductDetailsViewModel productDetailsViewModel = this.productsService.GetById(id).To<ProductDetailsViewModel>();

            return this.View(productDetailsViewModel);
        }
    }
}