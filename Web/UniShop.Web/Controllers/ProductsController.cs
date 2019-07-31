using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniShop.Web.ViewModels;

namespace UniShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        [HttpGet("/Products/Details/{id}")]
        public IActionResult Details(int id)
        {
            

            return this.View(new ProductDetailsViewModel());
        }
    }
}