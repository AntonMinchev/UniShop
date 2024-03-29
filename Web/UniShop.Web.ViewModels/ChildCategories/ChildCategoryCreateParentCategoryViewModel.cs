﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Web.ViewModels.Common;

namespace UniShop.Web.ViewModels.ChildCategories
{
    public class ChildCategoryCreateParentCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = ViewModelsConstants.Name)]
        public string Name { get; set; }

    }
}
