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
    public class UniShopUsersServiceTests
    {
        private IUniShopUsersService uniShopUsersService;

        public UniShopUsersServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }


        private UniShopUser GetDummyData()
        {
            return new UniShopUser
            {

                Id = "9f2d1ed7-3551-42d8-a2fb-5f4fefa4aaee",
                FullName = "TestName TestName",
                PhoneNumber = "01234567891",
                UserName = "TestUser",
                ShoppingCart = new ShoppingCart { }
            };
           
        }

        private void SeedData(UniShopDbContext context)
        {
            context.AddRange(GetDummyData());
            context.SaveChangesAsync();
        }


        [Fact]
        public void GetUserById_WithExistentId_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "UniShopUserService GetUserById() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            SeedData(context);
            this.uniShopUsersService = new UniShopUsersService(context);


            UniShopUserServiceModel expectedResults = context.Users.First().To<UniShopUserServiceModel>();
            UniShopUserServiceModel actualResults = this.uniShopUsersService.GetUserById(expectedResults.Id);
            

            Assert.True(expectedResults.Id == actualResults.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResults.FullName == actualResults.FullName, errorMessagePrefix + " " + "FullName is not returned properly.");
            Assert.True(expectedResults.PhoneNumber == actualResults.PhoneNumber, errorMessagePrefix + " " + "PhoneNumber is not returned properly.");
            Assert.True(expectedResults.UserName == actualResults.UserName, errorMessagePrefix + " " + "UserName is not returned properly.");
            Assert.True(expectedResults.ShoppingCartId == actualResults.ShoppingCartId, errorMessagePrefix + " " + "ShoppingCartId is not returned properly.");

        }

        [Fact]
        public void GetUserById_WithNonExistentId_ShouldReturnNull()
        {
            string errorMessagePrefix = "UniShopUserService GetUserById() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            SeedData(context);
            this.uniShopUsersService = new UniShopUsersService(context);

            UniShopUserServiceModel actualData = this.uniShopUsersService.GetUserById("bd6b5598-2704-4786-8db17e214db5ba864");

            Assert.True(actualData == null, errorMessagePrefix);
        }


        [Fact]
        public void GetUserByUsername_WithExistentUsername_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "UniShopUserService GetUserByUsername() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            SeedData(context);
            this.uniShopUsersService = new UniShopUsersService(context);


            UniShopUserServiceModel expectedResults = context.Users.First().To<UniShopUserServiceModel>();
            UniShopUserServiceModel actualResults = this.uniShopUsersService.GetUserByUsername(expectedResults.UserName);


            Assert.True(expectedResults.Id == actualResults.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResults.FullName == actualResults.FullName, errorMessagePrefix + " " + "FullName is not returned properly.");
            Assert.True(expectedResults.PhoneNumber == actualResults.PhoneNumber, errorMessagePrefix + " " + "PhoneNumber is not returned properly.");
            Assert.True(expectedResults.UserName == actualResults.UserName, errorMessagePrefix + " " + "UserName is not returned properly.");
            Assert.True(expectedResults.ShoppingCartId == actualResults.ShoppingCartId, errorMessagePrefix + " " + "ShoppingCartId is not returned properly.");

        }

        [Fact]
        public void GetUserByUsername_WithNonExistentUserName_ShouldReturnNull()
        {
            string errorMessagePrefix = "UniShopUserService GetUserByUsername() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            SeedData(context);
            this.uniShopUsersService = new UniShopUsersService(context);

            UniShopUserServiceModel actualResults = this.uniShopUsersService.GetUserByUsername("UserName");

            Assert.True(actualResults == null, errorMessagePrefix);

        }
    }
}

      