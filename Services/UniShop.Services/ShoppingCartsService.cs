using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Services
{
    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly UniShopDbContext context;

        public ShoppingCartsService(UniShopDbContext context)
        {
            this.context = context;
        }

        IQueryable<ShoppingCartProductServiceModel> IShoppingCartsService.GetAllShoppingCartProducts(string username)
        {
            var user = this.context.Users.FirstOrDefault(u => u.UserName == username);


            var shoppingCartProducts = this.context.ShoppingCartProducts.Where(s => s.ShoppingCartId == user.ShoppingCartId).To<ShoppingCartProductServiceModel>();

            return shoppingCartProducts;
        }
    }
}
