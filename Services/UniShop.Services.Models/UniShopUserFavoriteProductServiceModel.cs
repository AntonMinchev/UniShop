using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;

namespace UniShop.Services.Models
{
    public class UniShopUserFavoriteProductServiceModel : IMapFrom<UniShopUserFavoriteProduct>
    {
        public string UniShopUserId { get; set; }
        public UniShopUserServiceModel UniShopUser { get; set; }

        public int ProductId { get; set; }
        public ProductServiceModel Product { get; set; }
    }
}
