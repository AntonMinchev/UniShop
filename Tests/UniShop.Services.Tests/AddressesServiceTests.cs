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
    public class AddressesServiceTests
    {
        private IAddressesService addressesService;

        public AddressesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        private List<UniShopUser> GetDummyData()
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

      

        private void SeedData(UniShopDbContext context)
        {
            context.AddRange(GetDummyData());
            context.SaveChangesAsync();
        }


        [Fact]
        public void AddAddress_WithCorrectData_ShouldSuccessfullyCreate()
        {
            string errorMessagePrefix = "AddressesService AddAddress() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.addressesService = new AddressesService(context, new UniShopUsersService(context));

            UniShopUser user = context.Users.First();

            AddressServiceModel testAddress = new AddressServiceModel
            {
                City = "SofiqTestCraete",
                Street = "TesterCreate",
                BuildingNumber = "223"
            };

            int expectedCount = user.Addresses.Count() + 1;

            bool actualResult = this.addressesService.AddAddress(testAddress, user.Id);
            int actualCount = user.Addresses.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void Create_WithNonExistentUser_ShouldReturnFalse()
        {
            string errorMessagePrefix = "AddressesService AddAddress() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.addressesService = new AddressesService(context, new UniShopUsersService(context));

            UniShopUser user = context.Users.First();

            AddressServiceModel testAddress = new AddressServiceModel
            {
                City = "SofiqTestCraete",
                Street = "TesterCreate",
                BuildingNumber = "223"
            };

            int expectedCount = user.Addresses.Count();

            bool actualResult = this.addressesService.AddAddress(testAddress, user.Id + new Guid());
            int actualCount = user.Addresses.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }



        [Fact]
        public void GetAddressesByUserName_WithDummyData_ShouldReturnCorrectResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.addressesService = new AddressesService(context, new UniShopUsersService(context));

            UniShopUser user = context.Users.First();
          
            List<AddressServiceModel> actualResults = this.addressesService.GetAddressesByUserName(user.UserName).ToList();
            int expectedResults = user.Addresses.Count();


            Assert.Equal(expectedResults, actualResults.Count());
        }


        [Fact]
        public void GetAddressesByUserName_WithZeroData_ShouldReturnEmptyResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.addressesService = new AddressesService(context, new UniShopUsersService(context));

            UniShopUser user = context.Users.Last();

            List<AddressServiceModel> actualResults = this.addressesService.GetAddressesByUserName(user.UserName).ToList();
            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());
        }



        [Fact]
        public void GetAddressesByUserName_WithNonExistentUser_ShouldReturnEmptyResults()
        {
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.addressesService = new AddressesService(context, new UniShopUsersService(context));

            UniShopUser user = context.Users.Last();

            List<AddressServiceModel> actualResults = this.addressesService.GetAddressesByUserName(user.UserName + "Test").ToList();
            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());
        }

    }
}
