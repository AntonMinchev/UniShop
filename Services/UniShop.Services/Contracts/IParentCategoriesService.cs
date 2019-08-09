using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Models;
using UniShop.Web.InputModels;

namespace UniShop.Services.Contracts
{
    public interface IParentCategoriesService
    {
        bool Create(ParentCategoryServiceModel parentCategoryServiceModel );

        IQueryable<ParentCategoryServiceModel> GetAllParentCategories();

       
    }
}
