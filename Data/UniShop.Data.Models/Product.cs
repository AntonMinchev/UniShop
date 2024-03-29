﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.ShoppingCartProducts = new HashSet<ShoppingCartProduct>();
            this.FavoriteProducts = new HashSet<UniShopUserFavoriteProduct>();
            this.Reviews = new HashSet<Review>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool IsInStock => this.Quantity > 0;

        public int ChildCategoryId { get; set; }
        public ChildCategory ChildCategory { get; set; }

        public string Image { get; set; }

        public virtual ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; }

        public virtual ICollection<UniShopUserFavoriteProduct> FavoriteProducts { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        
    }
}
