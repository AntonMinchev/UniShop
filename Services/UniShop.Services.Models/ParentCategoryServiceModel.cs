using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;
using UniShop.Web.InputModels;

namespace UniShop.Services.Models
{
    public class ParentCategoryServiceModel : IMapFrom<ParentCategory>,IMapTo<ParentCategory>,IMapTo<ParentCategoryEditInputModel>,IMapFrom<ParentCategoryEditInputModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ChildCategoryServiceModel> ChildCategories { get; set; }
    }
}
