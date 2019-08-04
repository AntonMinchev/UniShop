using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels
{
    public class CategoryViewModel : IMapFrom<ParentCategoryServiceModel>
    {
        public string Name { get; set; }

        public virtual ICollection<ChildCategoryServiceModel> ChildCategories { get; set; }
    }
}
