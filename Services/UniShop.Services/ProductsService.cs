using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data;
using UniShop.Data.Models;
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
    }
}
