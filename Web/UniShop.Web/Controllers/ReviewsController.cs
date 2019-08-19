using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniShop.Web.InputModels;

namespace UniShop.Web.Controllers
{
    public class ReviewsController : BaseController
    {
        public ReviewsController()
        {

        }


        [HttpPost]
        public IActionResult Add(ReviewCreateInputModel reviewCreateInputModel)
        {
            return View();
        }
    }
}