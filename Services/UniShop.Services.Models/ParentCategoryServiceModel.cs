﻿using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;

namespace UniShop.Services.Models
{
    public class ParentCategoryServiceModel : IMapFrom<ParentCategory>,IMapTo<ParentCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ChildCategoryServiceModel> ChildCategories { get; set; }
    }
}
