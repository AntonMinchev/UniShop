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
    public class FavoriteProductsServiceTests
    {
        private IFavoriteProductsService favoriteProductsService;

        public FavoriteProductsServiceTests()
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
                                    Quantity = 1,
                                    Description = "TestProductLenovo",
                                    Specification = "TestProductLenovo"
                                },
                                new Product
                                {
                                    Name = "Lenovo d50 2",
                                    Price = 1001,
                                    Quantity = 1,
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
        public void AddFavoriteProduct_WithCorrectData_ShouldSuccessfullyCreate()
        {
            string errorMessagePrefix = "FavoriteProductsService AddFavoriteProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.favoriteProductsService = new FavoriteProductsService(context, new UniShopUsersService(context));

            UniShopUser user = context.Users.First();
            int productId = context.Products.First().Id;         

            int expectedCount = user.FavoriteProducts.Count() + 1;

            bool actualResult = this.favoriteProductsService.AddFavoriteProduct(productId, user.UserName);
            int actualCount = user.FavoriteProducts.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void AddFavoriteProduct_WithExistentSameFavoriteProduct_ShouldReturnFalse()
        {
            string errorMessagePrefix = "FavoriteProductsService AddFavoriteProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.favoriteProductsService = new FavoriteProductsService(context, new UniShopUsersService(context));

            UniShopUser user = context.Users.First();
            int productId = context.Products.First().Id;

            UniShopUserFavoriteProduct testFavoriteProduct = new UniShopUserFavoriteProduct
            {
                UniShopUserId = user.Id,
                ProductId = productId
            };

            context.Add(testFavoriteProduct);
            context.SaveChanges();

            int expectedCount = user.FavoriteProducts.Count();

            bool actualResult = this.favoriteProductsService.AddFavoriteProduct(productId, user.UserName);
            int actualCount = user.FavoriteProducts.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void AddFavoriteProduct_WithNonExistentProduct_ShouldReturnFalse()
        {
            string errorMessagePrefix = "FavoriteProductsService AddFavoriteProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.favoriteProductsService = new FavoriteProductsService(context, new UniShopUsersService(context));

            UniShopUser user = context.Users.First();
            int productId = context.Products.Last().Id + 1;

            int expectedCount = user.FavoriteProducts.Count();

            bool actualResult = this.favoriteProductsService.AddFavoriteProduct(productId, user.UserName);
            int actualCount = user.FavoriteProducts.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void AddFavoriteProduct_WithNonExistentUser_ShouldReturnFalse()
        {
            string errorMessagePrefix = "FavoriteProductsService AddFavoriteProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.favoriteProductsService = new FavoriteProductsService(context, new UniShopUsersService(context));

            string nonExistentUsername = "NonExistentTestUsername";
            int productId = context.Products.First().Id;

            bool actualResult = this.favoriteProductsService.AddFavoriteProduct(productId, nonExistentUsername);
          
            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void GetAllFavoriteProductsByUserId_WithDummyData_ShouldReturnCorrectResults()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.favoriteProductsService = new FavoriteProductsService(context, new UniShopUsersService(context));

            string userId = context.Users.First().Id;
            int firstProductId = context.Products.First().Id;
            int secondProductId = context.Products.Last().Id;
            List<UniShopUserFavoriteProduct> testfavoriteProducts = new List<UniShopUserFavoriteProduct>
            {
                new UniShopUserFavoriteProduct
                {
                    UniShopUserId = userId,
                    ProductId = firstProductId
                },
                new UniShopUserFavoriteProduct
                {

                    UniShopUserId = userId,
                    ProductId = secondProductId
                }
            };

            context.AddRange(testfavoriteProducts);
            context.SaveChanges();

            List<UniShopUserFavoriteProductServiceModel> actualResults = this.favoriteProductsService.GetAllFavoriteProductsByUserId(userId).ToList();
            int expectedResults = 2;
            Assert.Equal(expectedResults, actualResults.Count());

        }


        [Fact]
        public void GetAllFavoriteProductsByUserId_WithZeroData_ShouldReturnEmptyResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.favoriteProductsService = new FavoriteProductsService(context, new UniShopUsersService(context));

            string userId = context.Users.First().Id;

            List<UniShopUserFavoriteProductServiceModel> actualResults = this.favoriteProductsService.GetAllFavoriteProductsByUserId(userId).ToList();
            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());

        }


        [Fact]
        public void GetAllFavoriteProductsByUserId_WithNonExistentUser_ShouldReturnEmptyResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.favoriteProductsService = new FavoriteProductsService(context, new UniShopUsersService(context));

            string nonExistentuserId = context.Users.Last().Id + new Guid();

            List<UniShopUserFavoriteProductServiceModel> actualResults = this.favoriteProductsService.GetAllFavoriteProductsByUserId(nonExistentuserId).ToList();
            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());
        }


        [Fact]
        public void RemoveFavoriteProduct_WithCorrectData_ShouldSuccessfullyCreate()
        {
            string errorMessagePrefix = "FavoriteProductsService RemoveFavoriteProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.favoriteProductsService = new FavoriteProductsService(context, new UniShopUsersService(context));

            UniShopUser user = context.Users.First();
            int firstProductId = context.Products.First().Id;
            int secondProductId = context.Products.Last().Id;
            List<UniShopUserFavoriteProduct> testfavoriteProducts = new List<UniShopUserFavoriteProduct>
            {
                new UniShopUserFavoriteProduct
                {
                    UniShopUserId = user.Id,
                    ProductId = firstProductId
                },
                new UniShopUserFavoriteProduct
                {

                    UniShopUserId = user.Id,
                    ProductId = secondProductId
                }
            };

            context.AddRange(testfavoriteProducts);
            context.SaveChanges();

            int expectedCount = user.FavoriteProducts.Count() - 1;

            bool actualResult = this.favoriteProductsService.RemoveFavoriteProduct(firstProductId, user.UserName);
            int actualCount = user.FavoriteProducts.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }




        [Fact]
        public void RemoveFavoriteProduct_WithNonExistentUser_ShouldSuccessfullyCreate()
        {
            string errorMessagePrefix = "FavoriteProductsService RemoveFavoriteProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.favoriteProductsService = new FavoriteProductsService(context, new UniShopUsersService(context));


            int productId = context.Products.First().Id;

            bool actualResult = this.favoriteProductsService.RemoveFavoriteProduct(productId, "TestTestEr1");

            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void RemoveFavoriteProduct_WithNonExistentProduct_ShouldSuccessfullyCreate()
        {
            string errorMessagePrefix = "FavoriteProductsService RemoveFavoriteProduct() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.favoriteProductsService = new FavoriteProductsService(context, new UniShopUsersService(context));

            UniShopUser user = context.Users.First();
            int productId = context.Products.Last().Id + 1;

            bool actualResult = this.favoriteProductsService.RemoveFavoriteProduct(productId, user.UserName);

            Assert.False(actualResult, errorMessagePrefix);
        }

    }
}
