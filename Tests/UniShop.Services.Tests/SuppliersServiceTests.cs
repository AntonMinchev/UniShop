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
    public class SuppliersServiceTests
    {
        private ISuppliersService suppliersService;

        public SuppliersServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }


        private List<Supplier> GetDummyData()
        {
            return new List<Supplier>
            {
                new Supplier
                {
                    Name = "Econt",
                    PriceToHome = 7.50M,
                    PriceToOffice = 5.50M
                },
               new Supplier
                {
                    Name = "Speedy",
                    PriceToHome = 5.50M,
                    PriceToOffice = 4.50M
                },
               new Supplier
                {
                    Name = "DHL",
                    PriceToHome = 7.90M,
                    PriceToOffice = 5.50M
                },
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
            string errorMessagePrefix = "SuppliersService Create() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            SeedData(context);
            this.suppliersService = new SuppliersService(context);

            SupplierServiceModel testSupplier = new SupplierServiceModel
            {
                Name = "Econt",
                PriceToHome = 7.50M,
                PriceToOffice = 5.50M
            };

            bool actualResult = this.suppliersService.Create(testSupplier);
            Assert.True(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void Edit_WithCorrectData_ShouldPassSuccessfully()
        {
            string errorMessagePrefix = "SuppliersService Edit() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            SeedData(context);
            this.suppliersService = new SuppliersService(context);

            SupplierServiceModel expectedData = context.Suppliers.First().To<SupplierServiceModel>();

            bool actualData = this.suppliersService.Edit(expectedData);

            Assert.True(actualData, errorMessagePrefix);
        }

        [Fact]
        public void Edit_WithCorrectData_ShouldEditSupplierCorrectly()
        {
            string errorMessagePrefix = "SuppliersService Edit() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            SeedData(context);
            this.suppliersService = new SuppliersService(context);

            SupplierServiceModel expectedData = context.Suppliers.First().To<SupplierServiceModel>();

            expectedData.Name = "Editted_Name";
            expectedData.PriceToHome = 3.0M;
            expectedData.PriceToOffice = 2.0M;

            this.suppliersService.Edit(expectedData);

            SupplierServiceModel actualData = context.Suppliers.First().To<SupplierServiceModel>();

            Assert.True(actualData.Name == expectedData.Name, errorMessagePrefix + " " + "Name not editted properly.");
            Assert.True(actualData.PriceToHome == expectedData.PriceToHome, errorMessagePrefix + " " + "PriceToHome not editted properly.");
            Assert.True(actualData.PriceToOffice == expectedData.PriceToOffice, errorMessagePrefix + " " + "PriceToOffice not editted properly.");
           
        }

        [Fact]
        public void Delete_WithCorrectData_ShouldPassSuccessfully()
        {
            string errorMessagePrefix = "SuppliersService Delete() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            SeedData(context);
            this.suppliersService = new SuppliersService(context);

            int testId = context.Suppliers.First().Id;

            bool actualData = this.suppliersService.Delete(testId);

            Assert.True(actualData, errorMessagePrefix);
        }

        [Fact]
        public void Delete_WithNonExistentSupplierId_ShouldReturnFalse()
        {
            string errorMessagePrefix = "SuppliersService Delete() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            SeedData(context);
            this.suppliersService = new SuppliersService(context);

            int nonExistentId = 5;

            bool actualResult = this.suppliersService.Delete(nonExistentId);

            Assert.False(actualResult, errorMessagePrefix);
        }

        [Fact]
        public void GetAllSuppliers_WithDummyData_ShouldReturnCorrectResults()
        {
           
            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            SeedData(context);
            this.suppliersService = new SuppliersService(context);

            List<SupplierServiceModel> actualResults = this.suppliersService.GetAllSuppliers().ToList();
            int expectedResults = 3;

            Assert.Equal(expectedResults, actualResults.Count());
            
        }

        [Fact]
        public void GetAllSuppliers_WithZeroData_ShouldReturnEmptyResults()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
           
            this.suppliersService = new SuppliersService(context);

            List<SupplierServiceModel> actualResults = this.suppliersService.GetAllSuppliers().ToList();
            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());

        }


        [Fact]
        public void GetSupplierById_WithExistentId_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "SuppliersService GetSupplierById() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            SeedData(context);
            this.suppliersService = new SuppliersService(context);


            SupplierServiceModel expectedResults = context.Suppliers.First().To<SupplierServiceModel>();
            SupplierServiceModel actualResults = this.suppliersService.GetSupplierById(expectedResults.Id);


            Assert.True(expectedResults.Id == actualResults.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResults.Name == actualResults.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResults.PriceToHome == actualResults.PriceToHome, errorMessagePrefix + " " + "PriceToHome is not returned properly.");
            Assert.True(expectedResults.PriceToOffice == actualResults.PriceToOffice, errorMessagePrefix + " " + "PriceToOffice is not returned properly.");
           
        }


        [Fact]
        public void GetSupplierById_WithNonExistentId_ShouldReturnNull()
        {
            string errorMessagePrefix = "SuppliersService GetSupplierById() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            SeedData(context);
            this.suppliersService = new SuppliersService(context);

            int nonExistentId = 5;

            SupplierServiceModel actualResult = this.suppliersService.GetSupplierById(nonExistentId);

            Assert.True(actualResult == null, errorMessagePrefix);
        }

    }
}
