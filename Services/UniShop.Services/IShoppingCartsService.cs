using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Services.Models;

namespace UniShop.Services
{
    public interface IShoppingCartsService
    {
        IQueryable<ShoppingCartProductServiceModel> GetAllShoppingCartProducts(string username);
    }
}
