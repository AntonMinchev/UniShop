using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Models;
using UniShop.Web.InputModels;

namespace UniShop.Services
{
    public interface IParentCategoriesService
    {
        bool Create(ParentCategoryServiceModel parentCategoryServiceModel );

        IEnumerable<ParentCategory> GetAllParentCategories();

       
    }
}
