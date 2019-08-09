using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data;
using UniShop.Data.Models;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Services
{
    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly UniShopDbContext context;
        private readonly IUsersService usersService;
        private readonly IProductsService productsService;

        public ShoppingCartsService(UniShopDbContext context,IUsersService usersService,IProductsService productsService)
        {
            this.context = context;
            this.usersService = usersService;
            this.productsService = productsService;
        }

        public bool AddShoppingCartProduct(int productId, string username)
        {
            int result;

            var product = this.productsService.GetById(productId);

            var user = this.usersService.GetUserByUsername(username);

            var shoppingCartProduct = this.context.ShoppingCartProducts.FirstOrDefault(x => x.ProductId == productId && x.ShoppingCartId == user.ShoppingCartId);
           

            if (shoppingCartProduct == null && product.Quantity > 1)
            {
                var shoppingcartProduct = new ShoppingCartProduct
                {
                    ProductId = productId,
                    ShoppingCartId = user.ShoppingCartId,
                    Quantity = 1
                };



                context.ShoppingCartProducts.Add(shoppingcartProduct);
                result = context.SaveChanges();

                return result > 0;

                
            }
            else if (shoppingCartProduct != null && product.Quantity > shoppingCartProduct.Quantity )
            {
                shoppingCartProduct.Quantity++;

                this.context.Update(shoppingCartProduct);

                result = context.SaveChanges();

                return result > 0;
            }

            return false;
           
        }

        public bool IncreaseQuantity(int productId, string username)
        {
            var product = this.productsService.GetById(productId);

            var user = this.usersService.GetUserByUsername(username);

            var shoppingCartProduct = this.context.ShoppingCartProducts.FirstOrDefault(x => x.ProductId == productId && x.ShoppingCartId == user.ShoppingCartId);

            if (shoppingCartProduct == null)
            {
                throw new ArgumentNullException(nameof(shoppingCartProduct));
            }
            else if (product.Quantity > shoppingCartProduct.Quantity)
            {
                shoppingCartProduct.Quantity++;

                this.context.ShoppingCartProducts.Update(shoppingCartProduct);
                int result = this.context.SaveChanges();

                return result > 0;
            }
            else
            {
                return false;
            }


        }

        public bool ReduceQuantity(int productId, string username)
        {
            var product = this.productsService.GetById(productId);

            var user = this.usersService.GetUserByUsername(username);

            var shoppingCartProduct = this.context.ShoppingCartProducts.FirstOrDefault(x => x.ProductId == productId && x.ShoppingCartId == user.ShoppingCartId);

            if (shoppingCartProduct == null)
            {
                throw new ArgumentNullException(nameof(shoppingCartProduct));
            }
            else if (shoppingCartProduct.Quantity > 1)
            {
                shoppingCartProduct.Quantity--;

                this.context.ShoppingCartProducts.Update(shoppingCartProduct);
                int result = this.context.SaveChanges();

                return result > 0;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveShoppingCartProduct(int productId, string username)
        {
            var product = this.productsService.GetById(productId);

            var user = this.usersService.GetUserByUsername(username);

            var shoppingCartProduct = this.context.ShoppingCartProducts.FirstOrDefault(x => x.ProductId == productId && x.ShoppingCartId == user.ShoppingCartId);

            this.context.ShoppingCartProducts.Remove(shoppingCartProduct);
            int result = this.context.SaveChanges();

            return result > 0;
        }

        IQueryable<ShoppingCartProductServiceModel> IShoppingCartsService.GetAllShoppingCartProducts(string username)
        {
            var user = this.usersService.GetUserByUsername(username);


            var shoppingCartProducts = this.context.ShoppingCartProducts.Where(s => s.ShoppingCartId == user.ShoppingCartId).To<ShoppingCartProductServiceModel>();

            return shoppingCartProducts;
        }
    }
}
