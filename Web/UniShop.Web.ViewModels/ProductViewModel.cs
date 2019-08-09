using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels
{
    public class ProductViewModel : IMapFrom<ProductServiceModel>
    {
        public string Id { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public string ChildCategoryName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

    }
}
