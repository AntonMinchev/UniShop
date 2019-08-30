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
    public class ProductsServiceTests
    {
        private IProductsService productsService;

        public ProductsServiceTests()
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
                                    Name = "Lenovo d50",
                                    Price = 1000,
                                    Quantity = 1,
                                    Description = "TestProductLenovo",
                                    Specification = "TestProductLenovo",
                                    FavoriteProducts = new List<UniShopUserFavoriteProduct>(),
                                    Reviews = new List<Review>(),
                                    ShoppingCartProducts = new List<ShoppingCartProduct>()
                                },
                                 new Product
                                {
                                    Name = "Lenovo d50 2",
                                    Price = 1001,
                                    Quantity = 1,
                                    Description = "TestProductLenovo2",
                                    Specification = "TestProductLenovo2",
                                    FavoriteProducts = new List<UniShopUserFavoriteProduct>(),
                                    Reviews = new List<Review>(),
                                    ShoppingCartProducts = new List<ShoppingCartProduct>()
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


        [Fact]
        public void Create_WithCorrectData_ShouldSuccessfullyCreate()
        {
            string errorMessagePrefix = "ProductsService Create() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context,new ChildCategoriesService(context,new ParentCategoriesService(context)));

            int childCategoryId = context.ChildCategories.First().Id;

            ProductServiceModel testProduct = new ProductServiceModel
            {
                Name = "TestProduct",
                ChildCategoryId = childCategoryId
            };

            int expectedCount = context.Products.Count() + 1;

            bool actualResult = this.productsService.Create(testProduct);
            int actualCount = context.Products.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void Create_WithNonExistentParentCategory_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ProductsService Create() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));

            int nonExistentChildCategoryId = context.ChildCategories.Last().Id +1;

            ProductServiceModel testProduct = new ProductServiceModel
            {
                Name = "Lenovo",
                ChildCategoryId = nonExistentChildCategoryId
            };

            int expectedCount = context.ChildCategories.Count();

            bool actualResult = this.productsService.Create(testProduct);
            int actualCount = context.ChildCategories.Count();

            Assert.False(actualResult, errorMessagePrefix);
            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public void Edit_WithCorrectData_ShouldPassSuccessfully()
        {

            string errorMessagePrefix = "ProductsService Edit() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));


            ProductServiceModel productFromDb = context.Products.First().To<ProductServiceModel>();

            ProductServiceModel expectedProduct = productFromDb;
            expectedProduct.Name = "Edit";
            expectedProduct.Price = 0.01M;
            expectedProduct.Description = "TestProductEdit";
            expectedProduct.Specification = "TestProductEdit";
           

            bool actualResult = this.productsService.Edit(expectedProduct);

            ProductServiceModel actualProduct = context.Products.FirstOrDefault(p => p.Id == productFromDb.Id).To<ProductServiceModel>();

            Assert.True(actualProduct.Name == expectedProduct.Name, errorMessagePrefix + " " + "Name not editted properly.");
            Assert.True(actualProduct.Price == expectedProduct.Price, errorMessagePrefix + " " + "Price not editted properly.");
            Assert.True(actualProduct.Description == expectedProduct.Description, errorMessagePrefix + " " + "Description not editted properly.");
            Assert.True(actualProduct.Specification == expectedProduct.Specification, errorMessagePrefix + " " + "Specification not editted properly.");
            Assert.True(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void Edit_WithNonExistentId_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ProductsService Edit() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));


            ProductServiceModel productFromDb = context.Products.First().To<ProductServiceModel>();

            ProductServiceModel NonExistrentId = productFromDb;
            NonExistrentId.Id = 1000;

            bool actualResult = this.productsService.Edit(NonExistrentId);

            Assert.False(actualResult, errorMessagePrefix);
        }

        [Fact]
        public void Edit_WithNonExistentChildCategoryId_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ProductsService Edit() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));


            ProductServiceModel productFromDb = context.Products.First().To<ProductServiceModel>();

            ProductServiceModel NonExistrentId = productFromDb;
            NonExistrentId.ChildCategoryId = 1000;

            bool actualResult = this.productsService.Edit(NonExistrentId);

            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void GetAllProducts_WithoutParametarsWithDummyData_ShouldReturnCorrectResults()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));

            List<ProductServiceModel> actualResults = this.productsService.GetAllProducts().ToList();
            int expectedResults = 3;

            Assert.Equal(expectedResults, actualResults.Count());

        }


        [Fact]
        public void GetAllProducts_WithoutParametarsWithZeroData_ShouldReturnEmptyResults()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));

            List<ProductServiceModel> actualResults = this.productsService.GetAllProducts().ToList();
            int expectedResults = 0;

            Assert.Equal(expectedResults, actualResults.Count());

        }



        [Fact]
        public void GetAllProducts_WithChildCategoryIdWithoutSearchStringWithDummyData_ShouldReturnCorrectResults()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));

            var testChildCategory = context.ChildCategories.FirstOrDefault(c => c.Name == "Мебели");

            List<ProductServiceModel> actualResults = this.productsService.GetAllProducts(testChildCategory.Id,null).ToList();
            int expectedResults = 1;

            Assert.Equal(expectedResults, actualResults.Count());

        }


        [Fact]
        public void GetAllProducts_WithoutChildCategoryIdWithSearchStringWithDummyData_ShouldReturnCorrectResults()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));

            

            List<ProductServiceModel> actualResults = this.productsService.GetAllProducts(null, "Lenovo").ToList();
            int expectedResults = 2;

            Assert.Equal(expectedResults, actualResults.Count());

        }


        [Fact]
        public void GetById_WithExistentId_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ProductsService GetById() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));



            ProductServiceModel expectedResults = context.Products.First().To<ProductServiceModel>();
            ProductServiceModel actualResults = this.productsService.GetById(expectedResults.Id);


            Assert.True(expectedResults.Id == actualResults.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResults.Name == actualResults.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResults.Price == actualResults.Price, errorMessagePrefix + " " + "Price Count is not returned properly.");
            Assert.True(expectedResults.Description == actualResults.Description, errorMessagePrefix + " " + "Description is not returned properly.");

        }



        [Fact]
        public void GetById_WithNonExistentId_ShouldReturnNull()
        {
            string errorMessagePrefix = "ProductsService GetById() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));

            int nonExistentId = 1000;

            ProductServiceModel actualResults = this.productsService.GetById(nonExistentId);


            Assert.True(actualResults == null, errorMessagePrefix);
        }



        [Fact]
        public void ReduceProductQuantity_WithCorrectData_ShouldPassSuccessfully()
        {

            string errorMessagePrefix = "ProductsService ReduceProductQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));


            ProductServiceModel productFromDb = context.Products.First().To<ProductServiceModel>();
            int expectedQuantity = productFromDb.Quantity - 1;
            bool actualResult = this.productsService.ReduceProductQuantity(productFromDb.Id,1);
            
            ProductServiceModel actualProduct = context.Products.FirstOrDefault(p => p.Id == productFromDb.Id).To<ProductServiceModel>();

            Assert.True(expectedQuantity == actualProduct.Quantity, errorMessagePrefix + " " + "Quantity not editted properly.");
            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public void ReduceProductQuantity_WithNonExistentQuantity_ShouldReturnFalse()
        {

            string errorMessagePrefix = "ProductsService ReduceProductQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));


            ProductServiceModel productFromDb = context.Products.First().To<ProductServiceModel>();
            int expectedQuantity = productFromDb.Quantity;
            bool actualResult = this.productsService.ReduceProductQuantity(productFromDb.Id, expectedQuantity + 1);

            ProductServiceModel actualProduct = context.Products.FirstOrDefault(p => p.Id == productFromDb.Id).To<ProductServiceModel>();

            Assert.True(expectedQuantity == actualProduct.Quantity, errorMessagePrefix + " " + "Quantity not editted properly.");
            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void ReduceProductQuantity_WithNonExistentProductId_ShouldReturnFalse()
        {

            string errorMessagePrefix = "ProductsService ReduceProductQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));


            int nonExistenProductId = context.Products.Last().Id + 1;
            
            bool actualResult = this.productsService.ReduceProductQuantity(nonExistenProductId,1);

            Assert.False(actualResult, errorMessagePrefix);
        }


        [Fact]
        public void SortProducts_WithDummyData_ShouldReturnOrderedProductsByPriceDescending()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));

            IQueryable<ProductServiceModel> dataFromDb = context.Products.To<ProductServiceModel>();
            List<ProductServiceModel> actualData = this.productsService.SortProducts(dataFromDb, ProductsSort.PriceDescending).ToList();
            List<ProductServiceModel> expectedData = dataFromDb.OrderByDescending(p => p.Price).ToList();

           

            for (int i = 0; i < expectedData.Count; i++)
            {
                Assert.Equal(expectedData[0].Id, actualData[0].Id);
                Assert.Equal(expectedData[0].Price, actualData[0].Price);
            }    
        }


        [Fact]
        public void SortProducts_WithDummyData_ShouldReturnOrderedProductsByPriceAscending()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));

            IQueryable<ProductServiceModel> dataFromDb = context.Products.To<ProductServiceModel>();
            List<ProductServiceModel> actualData = this.productsService.SortProducts(dataFromDb, ProductsSort.PriceAscending).ToList();
            List<ProductServiceModel> expectedData = dataFromDb.OrderBy(p => p.Price).ToList();



            for (int i = 0; i < expectedData.Count; i++)
            {
                Assert.Equal(expectedData[0].Id, actualData[0].Id);
                Assert.Equal(expectedData[0].Price, actualData[0].Price);
            }
        }


        [Fact]
        public void SortProducts_WithZeroData_ShouldReturnEmptyResult()
        {

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
           
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));

            IQueryable<ProductServiceModel> dataFromDb = context.Products.To<ProductServiceModel>();
            List<ProductServiceModel> actualData = this.productsService.SortProducts(dataFromDb, ProductsSort.PriceAscending).ToList();
            List<ProductServiceModel> expectedData = dataFromDb.OrderBy(p => p.Price).ToList();


            Assert.True(actualData.Count() == expectedData.Count());
            
        }


        [Fact]
        public void CheckIsInStockShoppingCartProducts_WithCorrectData_ShouldReturnTrue()
        {
            string errorMessagePrefix = "ProductsService ReduceProductQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));

          
            Product firstProduct = context.Products.First();
            Product secondProduct = context.Products.Last();
            UniShopUser user = new UniShopUser
            {
                UserName = "Test User",
                ShoppingCart = new ShoppingCart { }
            };

            context.Add(user);

            List<ShoppingCartProduct> testshoppingProducts = new List<ShoppingCartProduct>
            {
                new ShoppingCartProduct
                {
                    ShoppingCart = user.ShoppingCart,
                    Product = firstProduct,
                    Quantity = 1,
                    
                },
                new ShoppingCartProduct
                {
                    ShoppingCart = user.ShoppingCart,
                    Product = secondProduct,
                    Quantity = 1
                }
            };

            context.AddRange(testshoppingProducts);
            context.SaveChanges();

            List<ShoppingCartProductServiceModel> shoppingCartProducts = testshoppingProducts
                .To<ShoppingCartProductServiceModel>().ToList();

            bool actualResult = this.productsService.CheckIsInStockShoppingCartProducts(shoppingCartProducts);

            Assert.True(actualResult, errorMessagePrefix);
        }



        [Fact]
        public void CheckIsInStockShoppingCartProducts_WithNonExistentProductQuantity_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ProductsService ReduceProductQuantity() method does not work properly.";

            var context = UniShopDbContextInMemoryFactory.InitializeContext();
            this.SeedData(context);
            this.productsService = new ProductsService(context, new ChildCategoriesService(context, new ParentCategoriesService(context)));


            Product firstProduct = context.Products.First();
            Product secondProduct = context.Products.Last();
            UniShopUser user = new UniShopUser
            {
                UserName = "Test User",
                ShoppingCart = new ShoppingCart { }
            };

            context.Add(user);

            List<ShoppingCartProduct> testshoppingProducts = new List<ShoppingCartProduct>
            {
                new ShoppingCartProduct
                {
                    ShoppingCart = user.ShoppingCart,
                    Product = firstProduct,
                    Quantity = 1,

                },
                new ShoppingCartProduct
                {
                    ShoppingCart = user.ShoppingCart,
                    Product = secondProduct,
                    Quantity = 10
                }
            };

            context.AddRange(testshoppingProducts);
            context.SaveChanges();

            List<ShoppingCartProductServiceModel> shoppingCartProducts = testshoppingProducts
                .To<ShoppingCartProductServiceModel>().ToList();

            bool actualResult = this.productsService.CheckIsInStockShoppingCartProducts(shoppingCartProducts);

            Assert.False(actualResult, errorMessagePrefix);
        }

    }
}