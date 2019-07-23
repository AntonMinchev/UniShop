using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Data.Models
{
    public class UniShopUserFavoriteProduct
    {
        public string UniShopUserId { get; set; }
        public UniShopUser UniShopUser { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
