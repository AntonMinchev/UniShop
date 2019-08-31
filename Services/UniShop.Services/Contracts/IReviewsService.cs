using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Services.Models;

namespace UniShop.Services.Contracts
{
    public interface IReviewsService
    {
        bool Create(ReviewServiceModel reviewServiceModel,string userId);

        IQueryable<ReviewServiceModel> GetReviewsByProductId(int productId);

        bool Delete(int id);

    }
}
