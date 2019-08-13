using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.FavoriteProducts
{
    public class UniShopUserFavoriteProductViewModel : IMapFrom<UniShopUserFavoriteProductServiceModel>
    {
 
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductImage { get; set; }

        public decimal ProductPrice { get; set; }

    }
}
