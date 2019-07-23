using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Data.Models
{
    public class ChildCategory
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentCategoryId { get; set; }
        public ParentCategory ParentCategory { get; set; }

        public ICollection<Product> Products { get; set; }
       
    }
}
