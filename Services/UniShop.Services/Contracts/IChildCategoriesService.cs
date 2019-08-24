using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Services.Models;

namespace UniShop.Services.Contracts
{
    public interface IChildCategoriesService
    {
        bool Create(ChildCategoryServiceModel childCategoryServiceModel);

        IQueryable<ChildCategoryServiceModel> GetAllChildCategories();

        ChildCategoryServiceModel GetChildCategoryById(int id);

        bool Edit(ChildCategoryServiceModel childCategoryServiceModel);

        bool Delete(int id);

        bool IsHaveChildCategoryWhitId(int id);
    }
}
