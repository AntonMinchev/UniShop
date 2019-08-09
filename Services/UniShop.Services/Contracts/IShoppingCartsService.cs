using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Services.Models;

namespace UniShop.Services.Contracts
{
    public interface IShoppingCartsService
    {
        IQueryable<ShoppingCartProductServiceModel> GetAllShoppingCartProducts(string username);

        bool AddShoppingCartProduct(int productId,string username);

        bool RemoveShoppingCartProduct(int productId, string username);

        bool IncreaseQuantity(int productId, string username);

        bool ReduceQuantity(int productId, string username);
    }
}
