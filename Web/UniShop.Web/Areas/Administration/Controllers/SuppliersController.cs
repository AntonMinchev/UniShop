using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UniShop.Web.Areas.Administration.Controllers
{
    public class SuppliersController : AdminController
    {
        [HttpGet("/Administration/Suppliers/Create")]
        public IActionResult Create()
        {
            return this.View("Create");
        }
    }
}