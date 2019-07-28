using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UniShop.Web.Areas.Administration.Controllers
{
    public class ChildCategoriesController : AdminController
    {
        [HttpGet("/Administration/ChildCategories/Create")]
        public IActionResult Create()
        {
            return this.View("Create");
        }
    }
}