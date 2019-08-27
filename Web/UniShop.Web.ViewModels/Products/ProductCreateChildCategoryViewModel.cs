using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Web.ViewModels.Products
{
    public class ProductCreateChildCategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ParentCategoryName { get; set; }
    }
}
