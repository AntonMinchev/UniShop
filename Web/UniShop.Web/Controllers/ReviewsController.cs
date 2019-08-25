using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services;
using UniShop.Services.Contracts;
using UniShop.Services.Models;
using UniShop.Web.InputModels;

namespace UniShop.Web.Controllers
{
    [Authorize]
    public class ReviewsController : BaseController
    {
        private readonly IReviewsService reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }


        [HttpPost]
        public IActionResult Add(ReviewCreateInputModel reviewCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return Redirect("/");
            }

            var review = AutoMapper.Mapper.Map<ReviewServiceModel>(reviewCreateInputModel);
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.reviewsService.Create(review,userId);

            return Redirect($"/Products/Details/{reviewCreateInputModel.ProductId}");
        }
    }
}