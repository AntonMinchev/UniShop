using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Common;

namespace UniShop.Web.ViewModels.Products
{
    public class ProductAllViewModel : IMapFrom<ProductServiceModel>
    {
        [Display(Name = ViewModelsConstants.ProductId)]
        public string Id { get; set; }

        [Display(Name = ViewModelsConstants.Image)]
        public string Image { get; set; }

        [Display(Name = ViewModelsConstants.Name)]
        public string Name { get; set; }

        [Display(Name = ViewModelsConstants.ChildCategoryName)]
        public string ChildCategoryName { get; set; }

        [Display(Name = ViewModelsConstants.Price)]
        public decimal Price { get; set; }

        [Display(Name = ViewModelsConstants.Quantity)]
        public int Quantity { get; set; }
    }
}
