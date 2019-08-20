using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;
using UniShop.Web.InputModels;

namespace UniShop.Services.Models
{
    public class ReviewServiceModel : IMapFrom<Review>,IMapTo<Review>, IMapFrom<ReviewCreateInputModel>
    {
        public int Id { get; set; }

        public string UniShopUserId { get; set; }
        public UniShopUser UniShopUser { get; set; }

        public int Raiting { get; set; }

        public string Comment { get; set; }

        public int ProductId { get; set; }
        public ProductServiceModel Product { get; set; }
    }
}
