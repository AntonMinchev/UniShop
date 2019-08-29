using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data;
using UniShop.Data.Models;
using UniShop.Services.Contracts;
using UniShop.Services.Models;
using UniShop.Services.Tests.Common;
using Xunit;

namespace UniShop.Services.Tests
{
    public class ShoppingCartsServiceTests
    {
        private IShoppingCartsService shoppingCartsService;

        public ShoppingCartsServiceTests()
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
                    ShoppingCart = new ShoppingCart { }
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

        private void SeedData(UniShopDbContext context)
        {
            context.AddRange(GetDummyDataUsers());
            context.AddRange(GetDummyData());
            context.SaveChangesAsync();
        }


        [Fact]
        public void AddShoppingCartProduct_WithCorrectData_ShouldSuccessfullyCreate()
        {
            string errorMessagePrefix = "ShoppingCartsService AddShoppingCartProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context,new ChildCategoriesService(context,new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.First().Id;

            int expectedCount = user.ShoppingCart.ShoppingCartProducts.Count() + 1;

            bool actualResult = this.shoppingCartsService.AddShoppingCartProduct(productId, user.UserName);
            int actualCount = user.ShoppingCart.ShoppingCartProducts.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void AddShoppingCartProduct_WithNonExistentProduct_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService AddShoppingCartProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.Last().Id + 1;

            int expectedCount = user.ShoppingCart.ShoppingCartProducts.Count();

            bool actualResult = this.shoppingCartsService.AddShoppingCartProduct(productId, user.UserName);
            int actualCount = user.ShoppingCart.ShoppingCartProducts.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void AddShoppingCartProduct_WithNonExistentUser_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService AddShoppingCartProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            string nonExistentUserId = context.Users.Last().Id + new Guid();
            int productId = context.Products.First().Id;

            bool actualResult = this.shoppingCartsService.AddShoppingCartProduct(productId, nonExistentUserId);
           
            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void AddShoppingCartProduct_WithExistentShoppingCartProductAndProductQuantityMoreThenOne_ShouldSuccessfullyIncreaseQuantity()
        {
            string errorMessagePrefix = "ShoppingCartsService AddShoppingCartProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.First().Id;

            ShoppingCartProduct testShoppingCartProduct = new ShoppingCartProduct
            {
                ProductId = productId,
                ShoppingCartId = user.ShoppingCartId,
                Quantity = 1
            };

            context.Add(testShoppingCartProduct);
            context.SaveChanges();

            ShoppingCartProduct shoppingCartProduct = context.ShoppingCartProducts
                .First(s => s.ShoppingCartId == user.ShoppingCartId && s.ProductId == productId);

            int expectedCount = shoppingCartProduct.Quantity + 1;

            bool actualResult = this.shoppingCartsService.AddShoppingCartProduct(productId, user.UserName);
            int actualCount = shoppingCartProduct.Quantity;

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }



        [Fact]
        public void AddShoppingCartProduct_WithExistentShoppingCartProductAndProductQuantitynOne_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService AddShoppingCartProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.Last().Id;

            ShoppingCartProduct testShoppingCartProduct = new ShoppingCartProduct
            {
                ProductId = productId,
                ShoppingCartId = user.ShoppingCartId,
                Quantity = 1
            };

            context.Add(testShoppingCartProduct);
            context.SaveChanges();

            int expectedCount = testShoppingCartProduct.Quantity;

            bool actualResult = this.shoppingCartsService.AddShoppingCartProduct(productId, user.UserName);
            int actualCount = testShoppingCartProduct.Quantity;

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }



        [Fact]
        public void AddShoppingCartProduct_WithProductQuantityZero_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService AddShoppingCartProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.Where(p => p.Quantity == 0).First().Id;

            int expectedCount = user.ShoppingCart.ShoppingCartProducts.Count();

            bool actualResult = this.shoppingCartsService.AddShoppingCartProduct(productId, user.UserName);
            int actualCount = user.ShoppingCart.ShoppingCartProducts.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }




        [Fact]
        public void IncreaseQuantity_WithCorrectData_ShouldSuccessfullyIncleaseQuantity()
        {
            string errorMessagePrefix = "ShoppingCartsService IncreaseQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.First().Id;

            ShoppingCartProduct testShoppingCartProduct = new ShoppingCartProduct
            {
                ProductId = productId,
                ShoppingCartId = user.ShoppingCartId,
                Quantity = 1
            };

            context.Add(testShoppingCartProduct);
            context.SaveChanges();

            int expectedCount = testShoppingCartProduct.Quantity + 1;

            bool actualResult = this.shoppingCartsService.IncreaseQuantity(productId, user.UserName);
            int actualCount = testShoppingCartProduct.Quantity;

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }



        [Fact]
        public void IncreaseQuantity_WithProductQuantitynOneAndShoppingCartProductQuantityOne_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService IncreaseQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.Where(p => p.Quantity == 0).First().Id;

            ShoppingCartProduct testShoppingCartProduct = new ShoppingCartProduct
            {
                ProductId = productId,
                ShoppingCartId = user.ShoppingCartId,
                Quantity = 1
            };

            context.Add(testShoppingCartProduct);
            context.SaveChanges();

            int expectedCount = testShoppingCartProduct.Quantity;

            bool actualResult = this.shoppingCartsService.IncreaseQuantity(productId, user.UserName);
            int actualCount = testShoppingCartProduct.Quantity;

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void IncreaseQuantity_WithNonExistentProduct_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService IncreaseQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.Last().Id + 1;

            bool actualResult = this.shoppingCartsService.IncreaseQuantity(productId, user.UserName);
         
            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void IncreaseQuantity_WithNonExistentUser_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService IncreaseQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            int productId = context.Products.First().Id;

            bool actualResult = this.shoppingCartsService.IncreaseQuantity(productId, "TestNonExistUsername");

            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void IncreaseQuantity_WithNonExistentShoppingCartProduct_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService IncreaseQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.First().Id;

            bool actualResult = this.shoppingCartsService.IncreaseQuantity(productId, user.UserName);

            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void ReduceQuantity_WithCorrectData_ShouldSuccessfullyReduceQuantity()
        {
            string errorMessagePrefix = "ShoppingCartsService ReduceQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.First().Id;

            ShoppingCartProduct testShoppingCartProduct = new ShoppingCartProduct
            {
                ProductId = productId,
                ShoppingCartId = user.ShoppingCartId,
                Quantity = 2
            };

            context.Add(testShoppingCartProduct);
            context.SaveChanges();

            int expectedCount = testShoppingCartProduct.Quantity - 1;

            bool actualResult = this.shoppingCartsService.ReduceQuantity(productId, user.UserName);
            int actualCount = testShoppingCartProduct.Quantity;

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void ReduceQuantity_WithShoppingCartProductQuantityOne_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService ReduceQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.Where(p => p.Quantity == 0).First().Id;

            ShoppingCartProduct testShoppingCartProduct = new ShoppingCartProduct
            {
                ProductId = productId,
                ShoppingCartId = user.ShoppingCartId,
                Quantity = 1
            };

            context.Add(testShoppingCartProduct);
            context.SaveChanges();

            int expectedCount = testShoppingCartProduct.Quantity;

            bool actualResult = this.shoppingCartsService.ReduceQuantity(productId, user.UserName);
            int actualCount = testShoppingCartProduct.Quantity;

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void ReduceQuantity_WithNonExistentProduct_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService ReduceQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.Last().Id + 1;

            bool actualResult = this.shoppingCartsService.ReduceQuantity(productId, user.UserName);

            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void ReduceQuantity_WithNonExistentUser_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService ReduceQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            int productId = context.Products.First().Id;

            bool actualResult = this.shoppingCartsService.ReduceQuantity(productId, "TestNonExistUsername");

            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void ReduceQuantity_WithNonExistentShoppingCartProduct_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService ReduceQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.First().Id;

            bool actualResult = this.shoppingCartsService.ReduceQuantity(productId, user.UserName);

            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void RemoveShoppingCartProduct_WithCorrectData_ShouldSuccessfullyRemove()
        {
            string errorMessagePrefix = "ShoppingCartsService RemoveShoppingCartProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.First().Id;

            ShoppingCartProduct testShoppingCartProduct = new ShoppingCartProduct
            {
                ProductId = productId,
                ShoppingCartId = user.ShoppingCartId,
                Quantity = 2
            };

            context.Add(testShoppingCartProduct);
            context.SaveChanges();

            int expectedCount = user.ShoppingCart.ShoppingCartProducts.Count() - 1 ;

            bool actualResult = this.shoppingCartsService.RemoveShoppingCartProduct(productId, user.UserName);
            int actualCount = user.ShoppingCart.ShoppingCartProducts.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }



        [Fact]
        public void RemoveShoppingCartProduct_WithNonExistentProduct_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService RemoveShoppingCartProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.Last().Id + 1;

            bool actualResult = this.shoppingCartsService.RemoveShoppingCartProduct(productId, user.UserName);

            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void RemoveShoppingCartProduct_WithNonExistentUser_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService RemoveShoppingCartProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            string nonExistentUserId = context.Users.Last().Id + new Guid();
            int productId = context.Products.First().Id;

            bool actualResult = this.shoppingCartsService.RemoveShoppingCartProduct(productId, nonExistentUserId);

            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void RemoveShoppingCartProduct_WithNonExistentShoppingCartProduct_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ShoppingCartsService RemoveShoppingCartProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int productId = context.Products.First().Id;

            bool actualResult = this.shoppingCartsService.RemoveShoppingCartProduct(productId, user.UserName);
            
            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void GetAllShoppingCartProducts_WithDummyData_ShouldReturnCorrectResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();
            int firstProductId = context.Products.First().Id;
            int secondProductId = context.Products.Last().Id;
            List<ShoppingCartProduct> testshoppingProducts = new List<ShoppingCartProduct>
            {
                new ShoppingCartProduct
                {
                    ShoppingCartId = user.ShoppingCartId,
                    ProductId = firstProductId,
                    Quantity = 1
                },
                new ShoppingCartProduct
                {
                    ShoppingCartId = user.ShoppingCartId,
                    ProductId = secondProductId,
                    Quantity = 1
                }
            };

            context.AddRange(testshoppingProducts);
            context.SaveChanges();

            List<ShoppingCartProductServiceModel> actualResults = this.shoppingCartsService
                .GetAllShoppingCartProducts(user.UserName).ToList();

            int expectedResults = 2;

            Assert.Equal(expectedResults, actualResults.Count());

        }


        [Fact]
        public void GetAllShoppingCartProducts_WithZeroData_ShouldReturnEmptyResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();

            List<ShoppingCartProductServiceModel> actualResults = this.shoppingCartsService
                .GetAllShoppingCartProducts(user.UserName).ToList();

            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());
        }


        [Fact]
        public void GetAllShoppingCartProducts_WithNonExistentUser_ShouldReturnEmptyResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.shoppingCartsService = new ShoppingCartsService(context, new UniShopUsersService(context),
                new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context))));

            UniShopUser user = context.Users.First();

            List<ShoppingCartProductServiceModel> actualResults = this.shoppingCartsService
                .GetAllShoppingCartProducts(user.UserName).ToList();

            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());
        }

    }
}