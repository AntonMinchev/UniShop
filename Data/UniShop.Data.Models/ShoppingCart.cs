using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Data.Models
{
    public class ShoppingCart
    {

        public int Id { get; set; }

        public UniShopUser User { get; set; }

        public virtual ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; }
       
    }
}
