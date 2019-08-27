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
    public class ParentCategoriesServiceTests
    {
        private IParentCategoriesService  parentCategoriesService;

        public ParentCategoriesServiceTests()
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
                            Name = "Лаптопи"
                        },
                        new ChildCategory
                        {
                            Name = "Настолни компютри"
                        }
                    }
                },
                 new ParentCategory
                {
                    Name = "За дома"
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
            string errorMessagePrefix = "ParentCategoriesService Create() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.parentCategoriesService = new ParentCategoriesService(context);

            ParentCategoryServiceModel testParentCategory = new ParentCategoryServiceModel
            {
                Name = "Книги"
            };

            int expectedCount = context.ParentCategories.Count() + 1;

            bool actualResult = this.parentCategoriesService.Create(testParentCategory);
            int actualCount = context.ParentCategories.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void Create_WithEmptyString_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ParentCategoriesService Create() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.parentCategoriesService = new ParentCategoriesService(context);

            ParentCategoryServiceModel testParentCategory = new ParentCategoryServiceModel
            {
                Name = string.Empty
            };

            int expectedCount = context.ParentCategories.Count();

            bool actualResult = this.parentCategoriesService.Create(testParentCategory);
            int actualCount = context.ParentCategories.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void Create_WithNullName_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ParentCategoriesService Create() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.parentCategoriesService = new ParentCategoriesService(context);

            ParentCategoryServiceModel testParentCategory = new ParentCategoryServiceModel
            {
                Name = null
            };

            int expectedCount = context.ParentCategories.Count();

            bool actualResult = this.parentCategoriesService.Create(testParentCategory);
            int actualCount = context.ParentCategories.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void Create_WithWhiteSpaceForName_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ParentCategoriesService Create() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.parentCategoriesService = new ParentCategoriesService(context);

            ParentCategoryServiceModel testParentCategory = new ParentCategoryServiceModel
            {
                Name = "      "
            };

            int expectedCount = context.ParentCategories.Count();

            bool actualResult = this.parentCategoriesService.Create(testParentCategory);
            int actualCount = context.ParentCategories.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void Delete_WithExistentIdAndWithoutChildCategories_ShouldPassSuccessfully()
        {
            string errorMessagePrefix = "ParentCategoriesService Delete() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.parentCategoriesService = new ParentCategoriesService(context);

            int testId = context.ParentCategories.Last().Id;
            int expectedCount = context.ParentCategories.Count() - 1;
            bool actualResult = this.parentCategoriesService.Delete(testId);

            int actualCount = context.ParentCategories.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void Delete_WithExistentIdAndChildCategories_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ParentCategoriesService Delete() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.parentCategoriesService = new ParentCategoriesService(context);

            int testId = context.ParentCategories.First().Id;
            int expectedCount = context.ParentCategories.Count();
            bool actualResult = this.parentCategoriesService.Delete(testId);

            int actualCount = context.ParentCategories.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void Delete_WithNonExistentId_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ParentCategoriesService Delete() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.parentCategoriesService = new ParentCategoriesService(context);

            int testId = context.ParentCategories.Last().Id + 1;
            int expectedCount = context.ParentCategories.Count();
            bool actualResult = this.parentCategoriesService.Delete(testId);

            int actualCount = context.ParentCategories.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void Edit_WithCorrectData_ShouldPassSuccessfully()
        {
            string errorMessagePrefix = "ParentCategoriesService Edit() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.parentCategoriesService = new ParentCategoriesService(context);

            ParentCategoryServiceModel parentCategoryFromDb = context.ParentCategories.First().To<ParentCategoryServiceModel>();

            ParentCategoryServiceModel expectedParentCategory = parentCategoryFromDb;
            expectedParentCategory.Name = "Edit";

            bool actualResult = this.parentCategoriesService.Edit(expectedParentCategory);

            ParentCategoryServiceModel actualParentCategory = context.ParentCategories.FirstOrDefault(p => p.Id == parentCategoryFromDb.Id).To<ParentCategoryServiceModel>();

            Assert.True(actualParentCategory.Name == expectedParentCategory.Name, errorMessagePrefix + " " + "Name not editted properly.");
            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public void Edit_WithNonExistentId_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ParentCategoriesService Edit() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.parentCategoriesService = new ParentCategoriesService(context);

            ParentCategoryServiceModel parentCategoryFromDb = context.ParentCategories.First().To<ParentCategoryServiceModel>();

            ParentCategoryServiceModel NonExistrentId = parentCategoryFromDb;
            NonExistrentId.Id = 10;

            bool actualResult = this.parentCategoriesService.Edit(NonExistrentId);

            Assert.False(actualResult, errorMessagePrefix);
        }

        [Fact]
        public void GetAllParentCategories_WithDummyData_ShouldReturnCorrectResults()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.parentCategoriesService = new ParentCategoriesService(context);

            List<ParentCategoryServiceModel> actualResults = this.parentCategoriesService.GetAllParentCategories().ToList();
            int expectedResults = 3;

            Assert.Equal(expectedResults, actualResults.Count());

        }

        [Fact]
        public void GetAllParentCategories_WithZeroData_ShouldReturnEmptyResults()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();

            this.parentCategoriesService = new ParentCategoriesService(context);


            List<ParentCategoryServiceModel> actualResults = this.parentCategoriesService.GetAllParentCategories().ToList();
            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());

        }


        [Fact]
        public void GetParenyCategoriesById_WithExistentId_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ParentCategoriesService GetParentCategoryById() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.parentCategoriesService = new ParentCategoriesService(context);


            ParentCategoryServiceModel expectedResults = context.ParentCategories.First().To<ParentCategoryServiceModel>();
            ParentCategoryServiceModel actualResults = this.parentCategoriesService.GetParentCategoryById(expectedResults.Id);


            Assert.True(expectedResults.Id == actualResults.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResults.Name == actualResults.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResults.ChildCategories.Count() == actualResults.ChildCategories.Count(), errorMessagePrefix + " " + "ChildCategories Count is not returned properly.");

        }

        [Fact]
        public void GetParentCategoryById_WithNonExistentId_ShouldReturnNull()
        {
            string errorMessagePrefix = "ParentCategoriesService GetParentCategoryById() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.parentCategoriesService = new ParentCategoriesService(context);

            int nonExistentId = 5;

            ParentCategoryServiceModel actualResults = this.parentCategoriesService.GetParentCategoryById(nonExistentId);

            Assert.True(actualResults == null, errorMessagePrefix);
        }

        [Fact]
        public void IsHaveParentCategory_WithExistentId_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ParentCategoriesService IsHaveParentCategory() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.parentCategoriesService = new ParentCategoriesService(context);

            int testId = context.ParentCategories.First().Id;

            bool actualResults = this.parentCategoriesService.IsHaveParentCategory(testId);

            Assert.True(actualResults, errorMessagePrefix);
        }

        [Fact]
        public void IsHaveParentCategory__WithNonExistentId_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ParentCategoriesService IsHaveParentCategory() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.parentCategoriesService = new ParentCategoriesService(context);

            int nonExistentId = context.ParentCategories.Last().Id + 1;

            bool actualResults = this.parentCategoriesService.IsHaveParentCategory(nonExistentId);

            Assert.False(actualResults, errorMessagePrefix);
        }
    }
}