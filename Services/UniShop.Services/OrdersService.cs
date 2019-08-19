﻿using System;
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

        public bool CreateOrder(string username, int supplierId, string deliveryType,int addressId)
        {
            var user = this.uniShopUsersService.GetUserByUsername(username);

            var order = new Order
            {
                OrderStatus = OrderStatus.Unprocessed,
                OrderDate = DateTime.UtcNow,
                DeliveryAddressId = addressId,
                EstimatedDeliveryDate = DateTime.UtcNow.AddDays(5),
                UniShopUserId = user.Id
            };

            this.context.Orders.Add(order);
            this.context.SaveChanges();

            var shoppingCartProducts = this.shoppingCartsService.GetAllShoppingCartProducts(username).ToList();

            var orderProducts = new List<OrderProduct>();

            foreach (var shoppingCartProduct in shoppingCartProducts)
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = shoppingCartProduct.ProductId,
                    Price = shoppingCartProduct.Product.Price,
                    Quantity = shoppingCartProduct.Quantity
                };

               bool isInStock = this.productsService.ReduceProductQuantity(shoppingCartProduct.ProductId, shoppingCartProduct.Quantity);

                if (!isInStock)
                {
                    return false;
                }

                orderProducts.Add(orderProduct);
            }

            this.context.OrderProducts.AddRange(orderProducts);

            var supplier = this.suppliersService.GetSupplierById(supplierId);

            if (deliveryType == "Home")
            {
                order.DeliveryPrice = supplier.PriceToHome;
            }
            else
            {
                order.DeliveryPrice = supplier.PriceToOffice;
            }

            order.TotalPrice = orderProducts.Sum(op => op.Price * op.Quantity) + order.DeliveryPrice;

            int result = this.context.SaveChanges();

            this.RemoveShoppingCartProducts(shoppingCartProducts);



            return result > 0;
        }

        public bool DeliverOrder(int id)
        {
            var order = this.context.Orders.FirstOrDefault(o => o.Id == id);

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

        public OrderServiceModel GetOrderById(int id)
        {
            var order = this.context.Orders.FirstOrDefault(o => o.Id == id).To<OrderServiceModel>();

            return order;
        }

        public bool ProcessingOrder(int id)
        {
            var order = this.context.Orders.FirstOrDefault(o => o.Id == id);

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