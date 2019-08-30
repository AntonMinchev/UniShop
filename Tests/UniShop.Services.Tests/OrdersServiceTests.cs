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
using UniShop.Services.Tests.Common;
using Xunit;

namespace UniShop.Services.Tests
{
    public class OrdersServiceTests
    {
        private IOrderService orderService;

        public OrdersServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        private List<UniShopUser> GetDummyDataUsers()
        {
            return new List<UniShopUser>
            {
                new UniShopUser
                {
                    UserName = "TestUser1",
                    FullName = "TestName TestName1",
                    PhoneNumber = "01234567891",
                    ShoppingCart = new ShoppingCart { },
                    Addresses = new List<Address>
                    {
                       new Address
                        {
                            City = "SofiqTest",
                            Street = "Tester",
                            BuildingNumber = "22"
                        },
                         new Address
                        {
                            City = "VarnaTest",
                            Street = "TesterTest",
                            BuildingNumber = "21"
                        }
                    }
                    
                },
                new UniShopUser
                {
                    UserName = "TestUser2",
                    FullName = "TestName TestName2",
                    PhoneNumber = "01234767891",
                    ShoppingCart = new ShoppingCart { }
                },
                new UniShopUser
                {
                     UserName = "TestUser3",
                    FullName = "TestName TestName3",
                    PhoneNumber = "01294567891",
                    ShoppingCart = new ShoppingCart { }
                }

            };

        }

        private List<ParentCategory> GetDummyData()
        {
            return new List<ParentCategory>
            {
                new ParentCategory
                {
                    Name = "Ит технологии",
                    ChildCategories = new List<ChildCategory>
                    {
                        new ChildCategory
                        {
                            Name = "Лаптопи",
                            Products = new List<Product>
                            {
                                new Product
                                {
                                    Name = "Lenovo d50",
                                    Price = 1000,
                                    Quantity = 2,
                                    Description = "TestProductLenovo",
                                    Specification = "TestProductLenovo"
                                },
                                new Product
                                {
                                    Name = "Lenovo d50 2",
                                    Price = 1001,
                                    Quantity = 0,
                                    Description = "TestProductLenovo2",
                                    Specification = "TestProductLenovo2"
                                }
                            }
                        }
                    }
                },
                new ParentCategory
                {
                    Name = "За дома",
                    ChildCategories = new List<ChildCategory>
                    {
                        new ChildCategory
                        {
                            Name = "Мебели",
                             Products = new List<Product>
                            {
                                new Product
                                {
                                    Name = "Маса",
                                    Price = 1002,
                                    Quantity = 1,
                                    Description = "TestProductMasa",
                                    Specification = "TestProductMasa"
                                }
                            }
                        }
                    }
                },
                new ParentCategory
                {
                    Name = "За автомобила"
                }
            };

        }

        private List<Supplier> GetDummyDataSuppliers()
        {
            return new List<Supplier>
            {
                new Supplier
                {
                    Name = "Ëcont",
                    PriceToHome = 7.50M,
                    PriceToOffice = 5M
                },

                new Supplier
                {
                    Name = "Speedy",
                    PriceToHome = 6.50M,
                    PriceToOffice = 4M
                }
            };

        }

        private void SeedData(UniShopDbContext context)
        {
            context.AddRange(GetDummyDataUsers());
            context.AddRange(GetDummyDataSuppliers());
            context.AddRange(GetDummyData());
            context.SaveChangesAsync();
        }


        [Fact]
        public void CreateOrder_WithCorrectData_ShouldSuccessfullyCreate()
        {
            string errorMessagePrefix = "OrdersService CreateOrder() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context,new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));
            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService,new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.First().Id;
            int supplierId = context.Suppliers.First().Id;
            int addressId = user.Addresses.First().Id;

            ShoppingCartProduct testShoppingCartProduct = new ShoppingCartProduct
            {
                ProductId = productId,
                ShoppingCartId = user.ShoppingCartId,
                Quantity = 1
            };

            context.Add(testShoppingCartProduct);
            context.SaveChanges();

            int expectedCount = user.Orders.Count() + 1;

            bool actualResult = this.orderService.CreateOrder(user.UserName,supplierId,1,addressId);
            int actualCount = user.Orders.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void CreateOrder_WithoutProductsInShoppingCart_ShouldReturnFalse()
        {
            string errorMessagePrefix = "OrdersService CreateOrder() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int supplierId = context.Suppliers.First().Id;
            int addressId = user.Addresses.First().Id;

            int expectedCount = user.Orders.Count();

            bool actualResult = this.orderService.CreateOrder(user.UserName, supplierId, 1, addressId);
            int actualCount = user.Orders.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void ProcessingOrder_WithCorrectData_ShouldProcessOrder()
        {
            string errorMessagePrefix = "OrdersService ProcessingOrder() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
    
            Order order = new Order
            {
                OrderStatus = OrderStatus.Unprocessed,
                UniShopUserId = user.Id,
            };

            context.Add(order);
            context.SaveChanges();

            OrderStatus expectedOrderStatus = OrderStatus.Processed;

            bool actualResult = this.orderService.ProcessingOrder(order.Id);
            OrderStatus actualOrderStatus = order.OrderStatus;

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedOrderStatus, actualOrderStatus);
        }


        [Fact]
        public void ProcessingOrder_WithNonExistentOrder_ShouldReturnFalse()
        {
            string errorMessagePrefix = "OrdersService ProcessingOrder() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));
           
            bool actualResult = this.orderService.ProcessingOrder(1000);
            
            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void DeliverOrder_WithCorrectData_ShouldChangeOrderStatusToDelivered()
        {
            string errorMessagePrefix = "OrdersService DeliverOrder() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();

            Order order = new Order
            {
                OrderStatus = OrderStatus.Processed,
                UniShopUserId = user.Id,
            };

            context.Add(order);
            context.SaveChanges();

            OrderStatus expectedOrderStatus = OrderStatus.Delivered;

            bool actualResult = this.orderService.DeliverOrder(order.Id);
            OrderStatus actualOrderStatus = order.OrderStatus;

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedOrderStatus, actualOrderStatus);
        }


        [Fact]
        public void DeliverOrder_WithNonExistentOrder_ShouldReturnFalse()
        {
            string errorMessagePrefix = "OrdersService DeliverOrder() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            bool actualResult = this.orderService.DeliverOrder(1000);

            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void GetAllOrdersByUserId_WithDummyData_ShouldReturnCorrectResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();

            List<Order> orders = new List<Order>
            {
                new Order
                {
                    OrderStatus = OrderStatus.Unprocessed,
                    UniShopUserId = user.Id,
                },
               new Order
               {
                    OrderStatus = OrderStatus.Unprocessed,
                    UniShopUserId = user.Id,
               }
            };

            context.AddRange(orders);
            context.SaveChanges();

            List<OrderServiceModel> actualResults = this.orderService
                .GetAllOrdersByUserId(user.Id).ToList();

            int expectedResults = 2;

            Assert.Equal(expectedResults, actualResults.Count());
        }


        [Fact]
        public void GetAllOrdersByUserId_WithNonExistentUser_ShouldReturnEmptyResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();

            List<OrderServiceModel> actualResults = this.orderService
                .GetAllOrdersByUserId(user.Id).ToList();

            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());
        }


        [Fact]
        public void GetAllDeliveredOrders_WithDummyData_ShouldReturnCorrectResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
          
            List<Order> orders = new List<Order>
            {
                new Order
                {
                    OrderStatus = OrderStatus.Delivered,
                    UniShopUserId = user.Id,
                },
               new Order
               {
                    OrderStatus = OrderStatus.Delivered,
                    UniShopUserId = user.Id,
               }
            };

            context.AddRange(orders);
            context.SaveChanges();

            List<OrderServiceModel> actualResults = this.orderService
                .GetAllDeliveredOrders().ToList();

            int expectedResults = 2;

            Assert.Equal(expectedResults, actualResults.Count());

        }



        [Fact]
        public void GetAllDeliveredOrders_WithZeroData_ShouldReturnEmptyResults()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            List<OrderServiceModel> actualResults = this.orderService
                           .GetAllDeliveredOrders().ToList();

            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());
        }


        [Fact]
        public void GetAllProcessedOrders_WithDummyData_ShouldReturnCorrectResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();

            List<Order> orders = new List<Order>
            {
                new Order
                {
                    OrderStatus = OrderStatus.Processed,
                    UniShopUserId = user.Id,
                },
               new Order
               {
                    OrderStatus = OrderStatus.Processed,
                    UniShopUserId = user.Id,
               }
            };

            context.AddRange(orders);
            context.SaveChanges();

            List<OrderServiceModel> actualResults = this.orderService
                .GetAllProcessedOrders().ToList();

            int expectedResults = 2;

            Assert.Equal(expectedResults, actualResults.Count());

        }



        [Fact]
        public void GetAllProcessedOrders_WithZeroData_ShouldReturnEmptyResults()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            List<OrderServiceModel> actualResults = this.orderService
                           .GetAllProcessedOrders().ToList();

            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());
        }


        [Fact]
        public void GetAllUnprocessedOrders_WithDummyData_ShouldReturnCorrectResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();

            List<Order> orders = new List<Order>
            {
                new Order
                {
                    OrderStatus = OrderStatus.Unprocessed,
                    UniShopUserId = user.Id,
                },
               new Order
               {
                    OrderStatus = OrderStatus.Unprocessed,
                    UniShopUserId = user.Id,
               }
            };

            context.AddRange(orders);
            context.SaveChanges();

            List<OrderServiceModel> actualResults = this.orderService
                .GetAllUnprocessedOrders().ToList();

            int expectedResults = 2;

            Assert.Equal(expectedResults, actualResults.Count());

        }



        [Fact]
        public void GetAllUnprocessedOrders_WithZeroData_ShouldReturnEmptyResults()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            List<OrderServiceModel> actualResults = this.orderService
                           .GetAllUnprocessedOrders().ToList();

            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());
        }


        [Fact]
        public void GetOrderByIdAndUserId_WithExistentId_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "OrdersService GetOrderByIdAndUserId() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();

            Order order = new Order
            {
                OrderStatus = OrderStatus.Processed,
                UniShopUserId = user.Id,
            };

            context.Add(order);
            context.SaveChanges();

            OrderServiceModel expectedResults = user.Orders.First().To<OrderServiceModel>();
            OrderServiceModel actualResults = this.orderService.GetOrderByIdAndUserId(expectedResults.Id,user.Id);


            Assert.True(expectedResults.Id == actualResults.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResults.OrderStatus == actualResults.OrderStatus, errorMessagePrefix + " " + "OrderStatus is not returned properly.");
            Assert.True(expectedResults.UniShopUserId == actualResults.UniShopUserId, errorMessagePrefix + " " + "UniShopUserId Count is not returned properly.");
        }

        [Fact]
        public void GetOrderByIdAndUserId_WithNonExistentOrder_ShouldReturnNull()
        {
            string errorMessagePrefix = "OrdersService GetOrderByIdAndUserId() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            UniShopUser user = context.Users.First();

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            int nonExistentId = 1000;

            OrderServiceModel actualResults = this.orderService.GetOrderByIdAndUserId(nonExistentId,user.Id);


            Assert.True(actualResults == null, errorMessagePrefix);
        }


        [Fact]
        public void GetOrderByIdAndUserId_WithNonCorrectData_ShouldReturnNull()
        {
            string errorMessagePrefix = "OrdersService GetOrderByIdAndUserId() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);

            string userId = context.Users.First().Id;

            string nonExistentUserId = userId + new Guid();

            Order order = new Order
            {
                OrderStatus = OrderStatus.Processed,
                UniShopUserId = userId,
            };

            context.Add(order);
            context.SaveChanges();

            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            this.orderService = new OrdersService(context, new UniShopUsersService(context), shoppingCartsService, new SuppliersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            OrderServiceModel actualResults = this.orderService.GetOrderByIdAndUserId(order.Id, nonExistentUserId);


            Assert.True(actualResults == null, errorMessagePrefix);
        }

    }
}