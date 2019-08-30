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
using UniShop.Web.InputModels;

namespace UniShop.Services
{
    public class ProductsService : IProductsService
    {
        private readonly UniShopDbContext context;
        private readonly IChildCategoriesService childCategoriesService;

        public ProductsService(UniShopDbContext context,IChildCategoriesService childCategoriesService)
        {
            this.context = context;
            this.childCategoriesService = childCategoriesService;
        }

        public bool Create(ProductServiceModel productServiceModel)
        {
            ChildCategory childCategoryFromDb = this.context.ChildCategories.FirstOrDefault(c => c.Id == productServiceModel.ChildCategoryId);

            if (childCategoryFromDb == null)
            {
                return false;
            }

            Product product = AutoMapper.Mapper.Map<Product>(productServiceModel);
            product.ChildCategory = childCategoryFromDb;

            context.Products.Add(product);
            int result = context.SaveChanges();

            return result > 0;
        }

        public bool Edit(ProductServiceModel productServiceModel)
        {
            var product = this.context.Products.FirstOrDefault(p => p.Id == productServiceModel.Id);

            if (product == null)
            {
                return false;
            }

            bool isHaveChildCategory = childCategoriesService.IsHaveChildCategoryWhitId(productServiceModel.ChildCategoryId);


            if (!isHaveChildCategory)
            {
                return false;
            } 

            product.Name = productServiceModel.Name;
            product.Price = productServiceModel.Price;
            product.Description = productServiceModel.Description;
            product.Specification = productServiceModel.Specification;
            product.Quantity = productServiceModel.Quantity;
            product.ChildCategoryId = productServiceModel.ChildCategoryId;
            if (!string.IsNullOrEmpty(productServiceModel.Image))
            {
                product.Image = productServiceModel.Image;
            }


            this.context.Products.Update(product);
            int result = context.SaveChanges();

            return result > 0;

        }

        public IQueryable<ProductServiceModel> GetAllProducts()
        {
            return this.context.Products.To<ProductServiceModel>();
        }

        public IQueryable<ProductServiceModel> GetAllProducts(int? childCategoryId, string searchString)
        {

            if (searchString != null)
            {
               return this.GetProductsBySearchString(searchString);

            }
            else if (childCategoryId != null)
            {
                return this.GetProductByChildCategoryId(childCategoryId);
            }

            return this.GetAllProducts();        
        }

        public ProductServiceModel GetById(int id)
        {
            var product = this.context.Products.To<ProductServiceModel>().FirstOrDefault(p => p.Id==id);

            return product;
        }

        public bool ReduceProductQuantity(int productId, int quantity)
        {
            var product = this.context.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return false;
            }

            var Quantity = product.Quantity - quantity;
            if (Quantity < 0)
            {
                return false;
            }
            product.Quantity = Quantity;
            this.context.Update(product);
            int result = this.context.SaveChanges();


            return result > 0;
        }

        public IQueryable<ProductServiceModel> SortProducts(IQueryable<ProductServiceModel> products, ProductsSort sort)
        {
            if (sort == ProductsSort.PriceDescending)
            {
                return products.OrderByDescending(p => p.Price);
            }

            return products.OrderBy(p => p.Price);
        }

        public bool CheckIsInStockShoppingCartProducts(List<ShoppingCartProductServiceModel> shoppingCartProducts)
        {
            foreach (var shoppingCartProduct in shoppingCartProducts)
            {

                int productQuantity = this.context.Products.First(p => p.Id == shoppingCartProduct.ProductId).Quantity;

                if (productQuantity < shoppingCartProduct.Quantity)
                {
                    return false;
                }
            }

            return true;
        }

        private IQueryable<ProductServiceModel> GetProductByChildCategoryId(int? categoryId)
        {

            return this.context.Products.Where(p => p.ChildCategoryId == categoryId).To<ProductServiceModel>();       
          
        }

        private IQueryable<ProductServiceModel> GetProductsBySearchString(string searchString)
        {
            var searchStringSplit = searchString.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var products = this.context.Products.Where(p =>searchStringSplit.All(s =>p.Name.ToLower().Contains(s.ToLower()))).To<ProductServiceModel>();

            return products;

        }
    }
}
