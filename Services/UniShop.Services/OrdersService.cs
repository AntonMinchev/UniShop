using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using UniShop.Data;
using UniShop.Data.Models;
using UniShop.Data.Models.Enums;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Services
{
    public class OrdersService : IOrderService
    {
        private const int EstimatedDeliverydays = 5;

        private readonly UniShopDbContext context;
        private readonly IUniShopUsersService uniShopUsersService;
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly ISuppliersService suppliersService;
        private readonly IProductsService productsService;

        public OrdersService(UniShopDbContext context,IUniShopUsersService uniShopUsersService,
            IShoppingCartsService shoppingCartsService,ISuppliersService suppliersService,IProductsService productsService)
        {
            this.context = context;
            this.uniShopUsersService = uniShopUsersService;
            this.shoppingCartsService = shoppingCartsService;
            this.suppliersService = suppliersService;
            this.productsService = productsService;
        }

        public bool CreateOrder(string username, int supplierId, int deliveryType,int addressId)
        {
            var user = this.uniShopUsersService.GetUserByUsername(username);

            if (user == null)
            {
                return false;
            }

            var shoppingCartProducts = this.shoppingCartsService.GetAllShoppingCartProducts(username).ToList();

            if (shoppingCartProducts.Count() == 0)
            {
                return false;
            }

            var orderProducts = new List<OrderProduct>();

            bool isInStock = this.productsService.CheckIsInStockShoppingCartProducts(shoppingCartProducts);

            if (!isInStock)
            {
                return false;
            }

            foreach (var shoppingCartProduct in shoppingCartProducts)
            {
                this.productsService.ReduceProductQuantity(shoppingCartProduct.ProductId, shoppingCartProduct.Quantity);
            }

            var order = new Order
            {
                OrderStatus = OrderStatus.Unprocessed,
                DeliveryType = deliveryType == 1 ? DeliveryType.ToHome : DeliveryType.ToOffice,
                OrderDate = DateTime.UtcNow,
                DeliveryAddressId = addressId,
                EstimatedDeliveryDate = DateTime.UtcNow.AddDays(EstimatedDeliverydays),
                UniShopUserId = user.Id,
                Recipient = user.FullName,
                RecipientPhoneNumber = user.PhoneNumber
            };

            this.context.Orders.Add(order);
            this.context.SaveChanges();

            foreach (var shoppingCartProduct in shoppingCartProducts)
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = shoppingCartProduct.ProductId,
                    Price = shoppingCartProduct.Product.Price,
                    Quantity = shoppingCartProduct.Quantity
                };

                orderProducts.Add(orderProduct);
            }

            this.context.OrderProducts.AddRange(orderProducts);

            var supplier = this.suppliersService.GetSupplierById(supplierId);

            if (order.DeliveryType == DeliveryType.ToHome)
            {
                order.DeliveryPrice = supplier.PriceToHome;
            }
            else
            {
                order.DeliveryPrice = supplier.PriceToOffice;
            }

            order.SupplierName = supplier.Name;
            order.TotalPrice = orderProducts.Sum(op => op.Price * op.Quantity) + order.DeliveryPrice;
            order.OrderProducts = orderProducts;
            int result = this.context.SaveChanges();

            this.RemoveShoppingCartProducts(shoppingCartProducts);

            return result > 0;
        }

        public bool DeliverOrder(int id)
        {
            var order = this.context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return false;
            }

            order.OrderStatus = OrderStatus.Delivered;
            order.DeliveryDate = DateTime.UtcNow;

            this.context.Update(order);
            int result = this.context.SaveChanges();

            return result > 0;
        }

        public IQueryable<OrderServiceModel> GetAllDeliveredOrders()
        {
            var deliveredOrders = this.context.Orders.Where(o => o.OrderStatus == OrderStatus.Delivered).To<OrderServiceModel>();

            return deliveredOrders;
        }

        public IQueryable<OrderServiceModel> GetAllOrdersByUserId(string userId)
        {
            var orders = this.context.Orders.Where(o => o.UniShopUserId == userId).To<OrderServiceModel>();

            return orders;
        }

        public IQueryable<OrderServiceModel> GetAllProcessedOrders()
        {
            var processedOrders = this.context.Orders.Where(o => o.OrderStatus == OrderStatus.Processed).To<OrderServiceModel>();

            return processedOrders;
        }

        public IQueryable<OrderServiceModel> GetAllUnprocessedOrders()
        {
            var unprocessedOrders = this.context.Orders.Where(o => o.OrderStatus == OrderStatus.Unprocessed).To<OrderServiceModel>();

            return unprocessedOrders;
        }

        public OrderServiceModel GetOrderByIdAndUserId(int id,string userId)
        {
            var order = this.context.Orders
                .Include(p => p.OrderProducts)
                .ThenInclude(x=>x.Product)
                .Include(o=>o.DeliveryAddress)
                .FirstOrDefault(o => o.Id == id && o.UniShopUserId == userId);

            if (order == null)
            {
                return null;
            }

            return order.To<OrderServiceModel>();
        }

        public bool ProcessingOrder(int id)
        {
            var order = this.context.Orders.FirstOrDefault(o => o.Id == id);


            if (order == null)
            {
                return false;
            }    
            

            order.OrderStatus = OrderStatus.Processed;
            order.DispatchDate = DateTime.UtcNow;

            this.context.Update(order);
            int result = this.context.SaveChanges();

            return result > 0;
        }

        private bool RemoveShoppingCartProducts(List<ShoppingCartProductServiceModel> shoppingCartProducts)
        {
            var shoppindCartProductsFromDb = new List<ShoppingCartProduct>();

            foreach (var product in shoppingCartProducts)
            {
                var shoppingProductFromDb = this.context.ShoppingCartProducts.FirstOrDefault(sp => sp.ProductId == product.ProductId && sp.ShoppingCartId == product.ShoppingCartId);

                shoppindCartProductsFromDb.Add(shoppingProductFromDb);
            }

            this.context.ShoppingCartProducts.RemoveRange(shoppindCartProductsFromDb);
            int result = this.context.SaveChanges();

            return result > 0;
        }

    }
}
