using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Products
{
    public class ProductViewModel : IMapFrom<ProductServiceModel>
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
