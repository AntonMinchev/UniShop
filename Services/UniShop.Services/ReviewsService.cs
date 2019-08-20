using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data;
using UniShop.Data.Models;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
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

        public bool Create(ReviewServiceModel reviewServiceModel,string userId)
        {
            var review = new Review
            {
                UniShopUserId = userId,
                ProductId = reviewServiceModel.ProductId,
                Comment = reviewServiceModel.Comment,
                Raiting = reviewServiceModel.Raiting
            };

            this.context.Reviews.Add(review);
            int result = this.context.SaveChanges();

            return result > 0;

        }

        public IQueryable<ReviewServiceModel> GetReviewsByProductId(int productId)
        {
            var reviews = this.context.Reviews.Where(r => r.ProductId == productId).To<ReviewServiceModel>();

            return reviews;
        }
    }
}
