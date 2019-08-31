using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services.Contracts;

namespace UniShop.Web.Areas.Administration.Controllers
{
    public class ReviewsController : AdminController
    {
        private readonly IReviewsService reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        public IActionResult Delete(int id ,int productId)
        {
            this.reviewsService.Delete(id);

            return Redirect($"/Products/Details/{productId}");
        }
    }
}