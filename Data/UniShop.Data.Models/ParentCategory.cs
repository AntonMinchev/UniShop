using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Data.Models
{
    public class ParentCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ChildCategory> ChildCategories { get; set; }
    }
}
