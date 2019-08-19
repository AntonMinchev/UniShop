using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniShop.Data;
using UniShop.Data.Models.Enums;
using UniShop.Services;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Web.InputModels;
using UniShop.Web.Models;
using UniShop.Web.ViewModels;
using UniShop.Web.ViewModels.Home;
using X.PagedList;

namespace UniShop.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index(IndexInputModel indexInputModel)
        {

            var pageSize = indexInputModel.PageSize ?? 9;
            var pageNumber = indexInputModel.PageNumber ?? 1;
                                             

            var products = this.productsService.GetAllProducts(indexInputModel.ChildCategoryId,indexInputModel.SearchString);
            var sortedProducts = this.productsService.SortProducts(products, indexInputModel.Sort).To<ProductHomeViewModel>().ToList();

            var pageSortedProducts = sortedProducts.ToPagedList(pageNumber, pageSize);
            
            var viewModel = new IndexViewModel
            {
                Products = pageSortedProducts,
                PageSize = pageSize,
                PageNumber = pageNumber,
                ChildCategoryId = indexInputModel.ChildCategoryId,
                Sort = indexInputModel.Sort,
                SearchString = indexInputModel.SearchString
            };

                //this.productsService.GetAllProducts()
                //   .Select(product => new ProductHomeViewModel
                //   {
                //       Id = product.Id,
                //       Name = product.Name,
                //       Price = product.Price,
                //       Image = product.Image
                //        //Raiting = product.Raiting    
                //    }).ToPagedList(pageNumber, 6); ;
            //ViewBag.OnePageOfProducts = products;

            return this.View(viewModel);

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
