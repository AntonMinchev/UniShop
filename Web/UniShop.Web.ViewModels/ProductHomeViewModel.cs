using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Web.ViewModels
{
    public class ProductHomeViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public int Raiting { get; set; }

        public int CountReviews { get; set; }
    }
}
