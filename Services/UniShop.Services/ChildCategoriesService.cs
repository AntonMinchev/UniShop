using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data;
using UniShop.Data.Models;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.InputModels;

namespace UniShop.Services
{
    public class ChildCategoriesService : IChildCategoriesService
    {
        private readonly UniShopDbContext context;
        private readonly IParentCategoriesService parentCategoriesService;

        public ChildCategoriesService(UniShopDbContext context,IParentCategoriesService parentCategoriesService)
        {
            this.context = context;
            this.parentCategoriesService = parentCategoriesService;
        }


        public bool Create(ChildCategoryServiceModel childCategoryServiceModel )
        {
           
            var isParentCategoryExists = this.parentCategoriesService.IsHaveParentCategory(childCategoryServiceModel.ParentCategoryId);

            if (!isParentCategoryExists)
            {
                return false;
            }


            ChildCategory childCategory = new ChildCategory
            {
                Name = childCategoryServiceModel.Name,
                ParentCategoryId = childCategoryServiceModel.ParentCategoryId
            };

            this.context.ChildCategories.Add(childCategory);
            int result = this.context.SaveChanges();


            return result > 0;
        }

        public bool Delete(int id)
        {
            var childCategory = this.context.ChildCategories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);

            if (childCategory == null)
            {
                return false;
            }

            var isHaveProducts = childCategory.Products.Any();

            if (isHaveProducts)
            {
                return false;
            }

            this.context.Remove(childCategory);
            int result = this.context.SaveChanges();

            return result > 0;
        }

        public bool Edit(ChildCategoryServiceModel childCategoryServiceModel)
        {
            var childCategory = this.context.ChildCategories.FirstOrDefault(c => c.Id == childCategoryServiceModel.Id);

            if (childCategory == null)
            {
                return false;
            }

            var parentCategory = this.context.ParentCategories.FirstOrDefault(p => p.Id == childCategoryServiceModel.ParentCategoryId);

            if (parentCategory == null)
            {
                return false;
            }

            childCategory.Name = childCategoryServiceModel.Name;
            childCategory.ParentCategoryId = childCategoryServiceModel.ParentCategoryId;

            this.context.Update(childCategory);
            int result = this.context.SaveChanges();

            return result > 0;
        }

        public IQueryable<ChildCategoryServiceModel> GetAllChildCategories()
        {
            var childCategories = this.context.ChildCategories.To<ChildCategoryServiceModel>();

            return childCategories;
        }

        public ChildCategoryServiceModel GetChildCategoryById(int id)
        {
            var childCategory = this.context.ChildCategories.To<ChildCategoryServiceModel>().FirstOrDefault(c => c.Id == id);

            return childCategory;
        }

        public bool IsHaveChildCategoryWhitId(int id)
        {
            return this.context.ChildCategories.Any(c => c.Id == id);
        }
    }
}
