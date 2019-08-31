using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data;
using UniShop.Data.Models;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Services.Tests.Common;
using Xunit;

namespace UniShop.Services.Tests
{
    public class ReviewsServiceTests
    {
        private IReviewsService reviewsService;

        public ReviewsServiceTests()
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
        public void Create_WithCorrectData_ShouldSuccessfullyCreate()
        {
            string errorMessagePrefix = "ReviewsService Create() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.reviewsService = new ReviewsService(context, new UniShopUsersService(context));

            string userId = context.Users.First().Id;
            int productId = context.Products.First().Id;

            ReviewServiceModel testReview = new ReviewServiceModel
            {
                 Raiting = 4,
                 ProductId = productId,
                 Comment = "Test Comment Test"
            };

            int expectedCount = context.Reviews.Count() + 1;

            bool actualResult = this.reviewsService.Create(testReview,userId);
            int actualCount = context.Reviews.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void Create_WithNonExistentProduct_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ReviewsService Create() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.reviewsService = new ReviewsService(context, new UniShopUsersService(context));

            string userId = context.Users.First().Id;
            int productId = context.Products.Last().Id + 1;

            ReviewServiceModel testReview = new ReviewServiceModel
            {
                Raiting = 2,
                ProductId = productId,
                Comment = "Test Comment Test"
            };

            int expectedCount = context.Reviews.Count();

            bool actualResult = this.reviewsService.Create(testReview,userId);
            int actualCount = context.Reviews.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void Create_WithNonExistentUser_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ReviewsService Create() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.reviewsService = new ReviewsService(context, new UniShopUsersService(context));

            string userId = context.Users.First().Id + new Guid();
            
            int productId = context.Products.Last().Id + 1;

            ReviewServiceModel testReview = new ReviewServiceModel
            {
                Raiting = 2,
                ProductId = productId,
                Comment = "Test Comment Test"
            };

            int expectedCount = context.Reviews.Count();

            bool actualResult = this.reviewsService.Create(testReview, userId);
            int actualCount = context.Reviews.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void GetReviewsByProductId_WithDummyData_ShouldReturnCorrectResults()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.reviewsService = new ReviewsService(context, new UniShopUsersService(context));

            string userId = context.Users.First().Id;
            int productId = context.Products.First().Id;
            List<Review> testReviews = new List<Review>
            {
                new Review
                {
                    Raiting = 4,
                    ProductId = productId,
                    Comment = "Test Comment Test",
                    UniShopUserId = userId
                },
                new Review
                {
                    Raiting = 5,
                    ProductId = productId,
                    Comment = "Test Comment Test2",
                    UniShopUserId = userId
                }
            };
           
            context.AddRange(testReviews);
            context.SaveChanges();

            List<ReviewServiceModel> actualResults = this.reviewsService.GetReviewsByProductId(productId).ToList();
            int expectedResults = 2;
            Assert.Equal(expectedResults, actualResults.Count());

        }


        [Fact]
        public void GetReviewsByProductId_WithZeroData_ShouldReturnEmptyResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.reviewsService = new ReviewsService(context, new UniShopUsersService(context));

            int productId = context.Products.First().Id;

            List<ReviewServiceModel> actualResults = this.reviewsService.GetReviewsByProductId(productId).ToList();
            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());

        }

        [Fact]
        public void GetReviewsByProductId_WithNonExistentProduct_ShouldReturnEmptyResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.reviewsService = new ReviewsService(context, new UniShopUsersService(context));

            int productId = context.Products.Last().Id + 1;

            List<ReviewServiceModel> actualResults = this.reviewsService.GetReviewsByProductId(productId).ToList();
            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());
        }


        [Fact]
        public void Delete_WithCorrectData_ShouldSuccessfullyDelete()
        {
            string errorMessagePrefix = "ReviewsService Delete() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.reviewsService = new ReviewsService(context, new UniShopUsersService(context));

            string userId = context.Users.First().Id;
            int productId = context.Products.First().Id;

            Review testReview = new Review
            {
                UniShopUserId = userId,
                Raiting = 4,
                ProductId = productId,
                Comment = "Test Comment Test"
            };

            context.Reviews.Add(testReview);
            context.SaveChanges();

            int expectedCount = context.Reviews.Count() - 1;
            bool actualResult = this.reviewsService.Delete(testReview.Id);
            int actualCount = context.Reviews.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }




        [Fact]
        public void Delete_WithNonExistentReview_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ReviewsService Delete() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.reviewsService = new ReviewsService(context, new UniShopUsersService(context));

            bool actualResult = this.reviewsService.Delete(1000);
            
            Assert.False(actualResult, errorMessagePrefix);
        }
    }
}
