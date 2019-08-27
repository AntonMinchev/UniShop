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
    public class ChildCategoriesServiceTests
    {
        private IChildCategoriesService childCategoriesService;

        public ChildCategoriesServiceTests()
        {
            MapperInitializer.InitializeMapper();
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
                                    Name = "Lenovo d50"
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
                            Name = "Мебели"
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


        [Fact]
        public void Create_WithCorrectData_ShouldSuccessfullyCreate()
        {
            string errorMessagePrefix = "ChildCategoriesService Create() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.childCategoriesService = new ChildCategoriesService(context,new ParentCategoriesService(context));

            int parentCategoryId = context.ParentCategories.First().Id;

            ChildCategoryServiceModel testChildCategory = new ChildCategoryServiceModel
            {
                Name = "Настолни компютри",
                ParentCategoryId = parentCategoryId
            };

            int expectedCount = context.ChildCategories.Count() + 1;

            bool actualResult = this.childCategoriesService.Create(testChildCategory);
            int actualCount = context.ChildCategories.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void Create_WithNonExistentParentCategory_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ChildCategoriesService Create() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.childCategoriesService = new ChildCategoriesService(context, new ParentCategoriesService(context));

            ChildCategoryServiceModel testChildCategory = new ChildCategoryServiceModel
            {
                Name = "Настолни компютри",
                ParentCategoryId = 10
            };

            int expectedCount = context.ChildCategories.Count();

            bool actualResult = this.childCategoriesService.Create(testChildCategory);
            int actualCount = context.ChildCategories.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void Delete_WithExistentIdAndWithoutProducts_ShouldPassSuccessfully()
        {
            string errorMessagePrefix = "ChildCategoriesService Delete() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.childCategoriesService = new ChildCategoriesService(context, new ParentCategoriesService(context));

            int testId = context.ChildCategories.Last().Id;
            int expectedCount = context.ChildCategories.Count() - 1;
            bool actualResult = this.childCategoriesService.Delete(testId);

            int actualCount = context.ChildCategories.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void Delete_WithExistentIdAndProducts_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ChildCategoriesService Delete() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.childCategoriesService = new ChildCategoriesService(context, new ParentCategoriesService(context));

            int testId = context.ChildCategories.First().Id;
            int expectedCount = context.ChildCategories.Count();
            bool actualResult = this.childCategoriesService.Delete(testId);

            int actualCount = context.ChildCategories.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }



        [Fact]
        public void Delete_WithNonExistentId_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ChildCategoriesService Delete() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.childCategoriesService = new ChildCategoriesService(context, new ParentCategoriesService(context));

            int testId = context.ChildCategories.Last().Id + 1;
            int expectedCount = context.ChildCategories.Count();
            bool actualResult = this.childCategoriesService.Delete(testId);

            int actualCount = context.ChildCategories.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void Edit_WithCorrectData_ShouldPassSuccessfully()
        {
            string errorMessagePrefix = "ChildCategoriesService Edit() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.childCategoriesService = new ChildCategoriesService(context, new ParentCategoriesService(context));

            ChildCategoryServiceModel childCategoryFromDb = context.ChildCategories.First().To<ChildCategoryServiceModel>();

            ChildCategoryServiceModel expectedChildCategory = childCategoryFromDb;
            expectedChildCategory.Name = "Edit";

            bool actualResult = this.childCategoriesService.Edit(expectedChildCategory);

            ChildCategoryServiceModel actualChildCategory = context.ChildCategories.FirstOrDefault(p => p.Id == childCategoryFromDb.Id).To<ChildCategoryServiceModel>();

            Assert.True(actualChildCategory.Name == expectedChildCategory.Name, errorMessagePrefix + " " + "Name not editted properly.");
            Assert.True(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void Edit_WithNonExistentId_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ChildCategoriesService Edit() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.childCategoriesService = new ChildCategoriesService(context, new ParentCategoriesService(context));

            ChildCategoryServiceModel childCategoryFromDb = context.ChildCategories.First().To<ChildCategoryServiceModel>();

            ChildCategoryServiceModel NonExistrentId = childCategoryFromDb;
            NonExistrentId.Id = 1000;

            bool actualResult = this.childCategoriesService.Edit(NonExistrentId);

            Assert.False(actualResult, errorMessagePrefix);
        }



        [Fact]
        public void GetAllChildCategories_WithDummyData_ShouldReturnCorrectResults()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.childCategoriesService = new ChildCategoriesService(context, new ParentCategoriesService(context));

            List<ChildCategoryServiceModel> actualResults = this.childCategoriesService.GetAllChildCategories().ToList();
            int expectedResults = 2;

            Assert.Equal(expectedResults, actualResults.Count());

        }


        [Fact]
        public void GetAllChildCategories_WithZeroData_ShouldReturnEmptyResults()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.childCategoriesService = new ChildCategoriesService(context, new ParentCategoriesService(context));

            List<ChildCategoryServiceModel> actualResults = this.childCategoriesService.GetAllChildCategories().ToList();
            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());

        }




        [Fact]
        public void GetChildCategoryById_WithExistentId_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ChildCategoriesService GetChildCategoryById() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.childCategoriesService = new ChildCategoriesService(context, new ParentCategoriesService(context));


            ChildCategoryServiceModel expectedResults = context.ChildCategories.First().To<ChildCategoryServiceModel>();
            ChildCategoryServiceModel actualResults = this.childCategoriesService.GetChildCategoryById(expectedResults.Id);


            Assert.True(expectedResults.Id == actualResults.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResults.Name == actualResults.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResults.Products.Count() == actualResults.Products.Count(), errorMessagePrefix + " " + "Products Count is not returned properly.");
            Assert.True(expectedResults.ParentCategoryId == actualResults.ParentCategoryId, errorMessagePrefix + " " + "ParentCategoryId is not returned properly.");

        }


        [Fact]
        public void GetChildCategoryById_WithNonExistentId_ShouldReturnNull()
        {
            string errorMessagePrefix = "ChildCategoriesService GetChildCategoryById() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.childCategoriesService = new ChildCategoriesService(context, new ParentCategoriesService(context));

            int nonExistentId = 5;

            ChildCategoryServiceModel actualResults = this.childCategoriesService.GetChildCategoryById(nonExistentId);


            Assert.True(actualResults == null, errorMessagePrefix);
        }

        [Fact]
        public void IsHaveChildCategoryWhitId_WithExistentId_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ChildCategoriesService IsHaveChildCategoryWhitId() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.childCategoriesService = new ChildCategoriesService(context, new ParentCategoriesService(context));

            int testId = context.ChildCategories.First().Id;

            bool actualResults = this.childCategoriesService.IsHaveChildCategoryWhitId(testId);

            Assert.True(actualResults, errorMessagePrefix);
        }


        [Fact]
        public void IsHaveChildCategoryWhitId__WithNonExistentId_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ChildCategoriesService IsHaveChildCategoryWhitId() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.childCategoriesService = new ChildCategoriesService(context, new ParentCategoriesService(context));


            int nonExistentId = context.ChildCategories.Last().Id + 1;

            bool actualResults = this.childCategoriesService.IsHaveChildCategoryWhitId(nonExistentId);

            Assert.False(actualResults, errorMessagePrefix);
        }

    }
}
