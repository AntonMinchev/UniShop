using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Web.Common;
using UniShop.Web.ViewModels;
using UniShop.Web.ViewModels.Products;
using X.PagedList;

namespace UniShop.Web.Controllers
{
  
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly IReviewsService reviewsService;

        public ProductsController(IProductsService productsService,IReviewsService reviewsService)
        {
            this.productsService = productsService;
            this.reviewsService = reviewsService;
        }

        [HttpGet("/Products/Details/{id}")]
        public IActionResult Details(int id,int? PageNumber)
        {
            var productViewModel = this.productsService.GetById(id).To<ProductViewModel>();

            var productReviews = this.reviewsService.GetReviewsByProductId(id).To<ProductReviewViewModel>().ToList();

            int pageNumber = PageNumber ?? WebConstants.DefaultPageNumber;
            
            var pageReviews = productReviews.ToPagedList(pageNumber, WebConstants.ReviewsPageSize);

            var productDetailsViewModel = new ProductDetailsViewModel
            {
                ProductViewModel = productViewModel,
                ProductReviews = pageReviews,
                ReviewCount = productReviews.Count()
            };

            return this.View(productDetailsViewModel);
        }
    }
}