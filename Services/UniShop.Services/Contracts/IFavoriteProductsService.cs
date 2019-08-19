using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Services.Models;

namespace UniShop.Services.Contracts
{
    public interface IFavoriteProductsService
    {
        bool AddFavoriteProduct(int productId, string username);

        bool RemoveFavoriteProduct(int productId, string username);

        IQueryable<UniShopUserFavoriteProductServiceModel> GetAllFavoriteProductsByUserId(string userId);
    }
}
