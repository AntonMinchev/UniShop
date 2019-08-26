using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace UniShop.Data.Models
{
    public class UniShopUser : IdentityUser<string>
    {

        public UniShopUser()
        {
            this.Addresses = new HashSet<Address>();
            this.Orders = new HashSet<Order>();
            this.FavoriteProducts = new HashSet<UniShopUserFavoriteProduct>();
            this.Reviews = new HashSet<Review>();
        }
        public string FullName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public virtual ICollection<UniShopUserFavoriteProduct> FavoriteProducts { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }


    }
}
