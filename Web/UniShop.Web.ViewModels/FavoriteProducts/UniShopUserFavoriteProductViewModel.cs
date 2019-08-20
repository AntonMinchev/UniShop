using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.FavoriteProducts
{
    public class UniShopUserFavoriteProductViewModel : IMapFrom<UniShopUserFavoriteProductServiceModel>
    {
 
        public int ProductId { get; set; }

        [Display(Name = "Име")]
        public string ProductName { get; set; }

        public string ProductImage { get; set; }

        [Display(Name = "Сума")]
        public decimal ProductPrice { get; set; }

    }
}
