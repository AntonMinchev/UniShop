using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.ChildCategories
{
    public class ChildCategoryNavbarViewModel : IMapFrom<ChildCategoryServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
