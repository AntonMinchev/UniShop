using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Models;

namespace UniShop.Services.Contracts
{
    interface IReviewsService
    {
        bool Create(ReviewServiceModel reviewServiceModel);
    }
}
