using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Services.Models;

namespace UniShop.Services
{
    public interface IChildCategoriesService
    {
        bool Create(ChildCategoryServiceModel childCategoryServiceModel);

        IQueryable<ChildCategoryServiceModel> GetAllChildCategoriesCategories();
    }
}
