using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data;
using UniShop.Data.Models;
using UniShop.Services.Contracts;
using UniShop.Services.Tests.Common;

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
                    UserName = "TestUser"
                },
                new UniShopUser
                {
                    UserName = "TestUser"
                },
                new UniShopUser
                {
                    UserName = "TestUser" 
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
                                    Specification = "TestProductLenovo",
                                   
                                },
                                new Product
                                {
                                    Name = "Lenovo d50 2",
                                    Price = 1001,
                                    Quantity = 1,
                                    Description = "TestProductLenovo2",
                                    Specification = "TestProductLenovo2",
                                
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
            context.AddRange(GetDummyData());
            context.SaveChangesAsync();
        }

    }
}
