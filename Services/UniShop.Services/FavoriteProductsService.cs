using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using UniShop.Data;
using UniShop.Data.Models;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Services
{
    public class FavoriteProductsService : IFavoriteProductsService
    {
        private readonly UniShopDbContext context;
        private readonly IUniShopUsersService uniShopUsersService;

        public FavoriteProductsService(UniShopDbContext context,IUniShopUsersService uniShopUsersService)
        {
            this.context = context;
            this.uniShopUsersService = uniShopUsersService;
        }

        public bool AddFavoriteProduct(int productId, string username)
        {
            var user = this.uniShopUsersService.GetUserByUsername(username);

            var isFavoriteProductExist = this.context.UniShopFavoriteProducts.Any(p => p.ProductId == productId);
            var isProductExist = this.context.Products.Any(p => p.Id == productId);

            if (user == null || isFavoriteProductExist || !isProductExist)
            {
                return false;
            }

            var favoriteProduct = new UniShopUserFavoriteProduct
            {
                ProductId = productId,
                UniShopUserId = user.Id
            };

            this.context.UniShopFavoriteProducts.Add(favoriteProduct);
            int result = this.context.SaveChanges();

            return result > 0;

        }

        public IQueryable<UniShopUserFavoriteProductServiceModel> GetAllFavoriteProductsByUserId(string userId)
        {
            var favoriteProducts = this.context.UniShopFavoriteProducts.Where(p => p.UniShopUserId == userId).To<UniShopUserFavoriteProductServiceModel>();

            return favoriteProducts;
        }

        public bool RemoveFavoriteProduct(int productId, string username)
        {
            var user = this.uniShopUsersService.GetUserByUsername(username);
            var favoriteProduct = this.context.UniShopFavoriteProducts.FirstOrDefault(p => p.ProductId == productId && p.UniShopUserId == user.Id);

            if (favoriteProduct == null)
            {
                return false;
            }

            this.context.UniShopFavoriteProducts.Remove(favoriteProduct);
            int result = this.context.SaveChanges();

            return result > 0;
        }
    }
}
