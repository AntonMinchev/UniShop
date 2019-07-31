using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Web.ViewModels
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsInStock { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        public string Image { get; set; }

        public int Quantity { get; set; }
    }
}
