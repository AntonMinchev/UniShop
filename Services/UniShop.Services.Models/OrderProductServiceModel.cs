using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;

namespace UniShop.Services.Models
{
    public class OrderProductServiceModel : IMapFrom<OrderProduct>
    {
        public int OrderId { get; set; }
        public OrderServiceModel Order { get; set; }

        public int ProductId { get; set; }
        public ProductServiceModel Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
