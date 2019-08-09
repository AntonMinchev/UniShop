﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services;
using UniShop.Services.Contracts;
using UniShop.Web.Models;
using UniShop.Web.ViewModels;
using UniShop.Web.ViewModels.Home;

namespace UniShop.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            List<ProductHomeViewModel> products = this.productsService.GetAllProducts()
                    .Select(product => new ProductHomeViewModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Image = product.Image
                        //Raiting = product.Raiting    
                    })
                    .ToList();

            return this.View(products);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PackageDelivery()
        {
            return View();
        }

        public IActionResult Warranty()
        {
            return View();
        }

        public IActionResult ReturnShipment()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
