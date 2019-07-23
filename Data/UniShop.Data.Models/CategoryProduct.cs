using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Data.Models
{
    public class CategoryProduct
    {

        public int ChildCategoryId { get; set; }
        public ChildCategory ChildCategory { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    
    }
}
