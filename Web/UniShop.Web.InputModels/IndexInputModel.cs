using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models.Enums;

namespace UniShop.Web.InputModels
{
    public class IndexInputModel
    {
        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public int? ChildCategoryId { get; set; }

        public ProductsSort Sort { get; set; }

        public string SearchString { get; set; }     
    }
}
