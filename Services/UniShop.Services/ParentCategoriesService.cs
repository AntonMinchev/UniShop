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
            if (string.IsNullOrEmpty(parentCategoryServiceModel.Name) ||
                string.IsNullOrWhiteSpace(parentCategoryServiceModel.Name) || 
                parentCategoryServiceModel == null)
            {
                return false;
            }

            ParentCategory parentCategory = new ParentCategory
            {
                Name = parentCategoryServiceModel.Name
            };

            context.ParentCategories.Add(parentCategory);
            int result = context.SaveChanges();

            return result > 0;
        }

        public bool Delete(int id)
        {
            var parentCategory = this.context.ParentCategories.Include(c => c.ChildCategories).FirstOrDefault(p => p.Id == id);

            if (parentCategory == null)
            {
                return false;
            }

            var isHaveChildCategories = parentCategory.ChildCategories.Any();

            if (isHaveChildCategories)
            {
                return false;
            }

            this.context.Remove(parentCategory);
            int result = this.context.SaveChanges();

            return result > 0;
        }

        public bool Edit(ParentCategoryServiceModel parentCategoryServiceModel)
        {
            var parentCategory = this.context.ParentCategories.FirstOrDefault(p => p.Id == parentCategoryServiceModel.Id);

            if (parentCategory == null)
            {
                return false;
            }

            parentCategory.Name = parentCategoryServiceModel.Name;
            this.context.Update(parentCategory);
            int result = this.context.SaveChanges();

            return result > 0;
        }

        public IQueryable<ParentCategoryServiceModel> GetAllParentCategories()
        {
            var parentCategories = this.context.ParentCategories.Include(x =>x.ChildCategories).To<ParentCategoryServiceModel>();

            return parentCategories;
        }

        public ParentCategoryServiceModel GetParentCategoryById(int id)
        {
            var parentCategory = this.context.ParentCategories.To<ParentCategoryServiceModel>().FirstOrDefault(p => p.Id == id);

            return parentCategory;
        }

        public bool IsHaveParentCategory(int id)
        {
            return this.context.ParentCategories.Any(p => p.Id == id);
        }
    }
}
