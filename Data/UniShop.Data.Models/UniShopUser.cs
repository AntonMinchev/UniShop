using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace UniShop.Data.Models
{
    public class UniShopUser : IdentityUser<string>
    {
        public UniShopUser()
        {
        }
                
        public string FullName { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }


        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public virtual ICollection<UniShopUserFavoriteProduct> FavoriteProducts { get; set; }

    }
}
