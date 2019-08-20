using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using X.PagedList;

namespace UniShop.Web.ViewModels.Products
{
    public class ProductDetailsViewModel 
    {
        public ProductViewModel ProductViewModel { get; set; }

        public IPagedList<ProductReviewViewModel>  ProductReviews { get; set; }

        public int ReviewCount { get; set; }
    }
}
