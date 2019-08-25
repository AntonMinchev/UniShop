using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Common;

namespace UniShop.Web.ViewModels.FavoriteProducts
{
    public class UniShopUserFavoriteProductViewModel : IMapFrom<UniShopUserFavoriteProductServiceModel>
    {
 
        [Display(Name = ViewModelsConstants.ProductId)]
        public int ProductId { get; set; }

        [Display(Name = ViewModelsConstants.Name)]
        public string ProductName { get; set; }

        [Display(Name = ViewModelsConstants.Image)]
        public string ProductImage { get; set; }

        [Display(Name = ViewModelsConstants.Price)]
        public decimal ProductPrice { get; set; }

    }
}
