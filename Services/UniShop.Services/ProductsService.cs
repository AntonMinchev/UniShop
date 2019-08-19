﻿using System;
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

        public ProductsService(UniShopDbContext context)
        {
            this.context = context;
        }

        public bool Create(ProductServiceModel productServiceModel)
        {
            var test = this.context.ChildCategories.Select(x => x.Products).ToList();

            ChildCategory childCategoryFromDb =
                context.ChildCategories
                .SingleOrDefault(childCategory => childCategory.Name == productServiceModel.ChildCategoryName);

            Product product = AutoMapper.Mapper.Map<Product>(productServiceModel);
            product.ChildCategory = childCategoryFromDb;

            context.Products.Add(product);
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
            var product = this.context.Products.FirstOrDefault(p => p.Id==id);

            return product.To<ProductServiceModel>();
        }

        public bool ReduceProductQuantity(int productId, int quantity)
        {
            var product = this.context.Products.FirstOrDefault(p => p.Id == productId);

            product.Quantity = product.Quantity - quantity;
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

        private IQueryable<ProductServiceModel> GetProductByChildCategoryId(int? categoryId)
        {

            return this.context.Products.Where(p => p.ChildCategoryId == categoryId).To<ProductServiceModel>();       
          
        }

        private IQueryable<ProductServiceModel> GetProductsBySearchString(string searchString)
        {
            return this.context.Products.Where(p => p.Name.ToLower().Contains(searchString.ToLower())).To<ProductServiceModel>();
        }
    }
}
