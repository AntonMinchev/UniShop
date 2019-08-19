using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Home
{
    public class ProductHomeViewModel : IMapFrom<ProductServiceModel>,IMapFrom<Product>
    {
     
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public int Raiting { get; set; }

        public int CountReviews { get; set; }
    }
}
