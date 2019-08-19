using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data;
using UniShop.Data.Models;
using UniShop.Services.Contracts;
using UniShop.Services.Models;

namespace UniShop.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly UniShopDbContext context;

        public ReviewsService(UniShopDbContext context)
        {
            this.context = context;
        }

        public bool Create(ReviewServiceModel reviewServiceModel)
        {
            var review = new Review
            {
                ProductId = reviewServiceModel.ProductId,
                Comment = reviewServiceModel.Comment,
                Raiting = reviewServiceModel.Raiting
            };

            return false;

        }
    }
}
