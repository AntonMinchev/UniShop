using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models.Enums;
using X.PagedList;

namespace UniShop.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }

        public int? ChildCategoryId { get; set; }

        public string SearchString { get; set; }

        public ProductsSort Sort { get; set; }

        public IPagedList<ProductHomeViewModel> Products { get; set; }

    }
}
