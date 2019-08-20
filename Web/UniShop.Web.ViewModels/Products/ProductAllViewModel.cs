using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Products
{
    public class ProductAllViewModel : IMapFrom<ProductServiceModel>
    {
        [Display(Name = "Номер")]
        public string Id { get; set; }

        public string Image { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Категория")]
        public string ChildCategoryName { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }
    }
}
