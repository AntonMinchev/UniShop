using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UniShop.Web.Areas.Administration.Controllers
{
    public class ParentCategoriesController : AdminController
    {
        [HttpGet("/Administration/ParentCategories/Create")]
        public IActionResult Create()
        {
            return this.View("Create");
        }
    }
}