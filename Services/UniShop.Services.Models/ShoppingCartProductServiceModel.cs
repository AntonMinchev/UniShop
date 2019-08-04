using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;

namespace UniShop.Services.Models
{
    public class ShoppingCartProductServiceModel : IMapFrom<ShoppingCartProduct>,IMapTo<ShoppingCartProduct>
    {
        public int ShoppingCartId { get; set; }

        public ShoppingCartServiceModel ShoppingCart { get; set; }

        public int ProductId { get; set; }
        public ProductServiceModel Product { get; set; }

        public int Quantity { get; set; }

    }
}
