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
    public class ChildCategoriesService : IChildCategoriesService
    {
        private readonly UniShopDbContext context;

        public ChildCategoriesService(UniShopDbContext context)
        {
            this.context = context;
        }


        public bool Create(ChildCategoryServiceModel childCategoryServiceModel )
        {
            ChildCategory childCategory = new ChildCategory
            {
                Name = childCategoryServiceModel.Name,
                ParentCategoryId = childCategoryServiceModel.ParentCategoryId
            };

            this.context.ChildCategories.Add(childCategory);
            int result = this.context.SaveChanges();


            return result > 0;
        }

        public IQueryable<ChildCategoryServiceModel> GetAllChildCategories()
        {
            var childCategories = this.context.ChildCategories.To<ChildCategoryServiceModel>();

            return childCategories;
        }
    }
}
