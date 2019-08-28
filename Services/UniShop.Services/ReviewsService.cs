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
        private readonly IUniShopUsersService uniShopUsersService;

        public ReviewsService(UniShopDbContext context,IUniShopUsersService uniShopUsersService)
        {
            this.context = context;
            this.uniShopUsersService = uniShopUsersService;
        }

        public bool Create(ReviewServiceModel reviewServiceModel,string userId)
        {
            var product = this.context.Products.FirstOrDefault(p => p.Id == reviewServiceModel.ProductId);

            if (product == null)
            {
                return false;
            }

            var user = this.uniShopUsersService.GetUserById(userId);

         
            if (user == null)
            {
                return false;
            }

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
            //var product = this.context.Products.FirstOrDefault(p => p.Id == productId);

            //if (product == null)
            //{
            //    return null;
            //}

            var reviews = this.context.Reviews.Where(r => r.ProductId == productId).To<ReviewServiceModel>();

            return reviews;
        }
    }
}
