using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Orders
{
    public class OrderProductViewModel :IMapFrom<OrderProductServiceModel>
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal Price { get; set; }

        public string ProductImage { get; set; }

        public int Quantity { get; set; }

     
    }
}
