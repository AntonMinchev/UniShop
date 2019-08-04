using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data;
using UniShop.Data.Models;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Services
{
    public class ParentCategoriesService : IParentCategoriesService
    {
        private readonly UniShopDbContext context;

        public ParentCategoriesService(UniShopDbContext context)
        {
            this.context = context;
        }
        public bool Create(ParentCategoryServiceModel parentCategoryServiceModel)
        {
            ParentCategory parentCategory = new ParentCategory
            {
                Name = parentCategoryServiceModel.Name
            };

            context.ParentCategories.Add(parentCategory);
            int result = context.SaveChanges();

            return result > 0;
        }


        IQueryable<ParentCategoryServiceModel> IParentCategoriesService.GetAllParentCategories()
        {
            var parentCategories = this.context.ParentCategories.To<ParentCategoryServiceModel>();

            return parentCategories;
        }
    }
}
