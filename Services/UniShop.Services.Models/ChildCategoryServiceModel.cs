using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;
using UniShop.Web.InputModels;

namespace UniShop.Services.Models
{
    public class ChildCategoryServiceModel :  IMapFrom<ChildCategoryCreateInputModel>,IMapFrom<ChildCategory>
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentCategoryId { get; set; }

        public ParentCategoryServiceModel ParentCategory { get; set; }

        public ICollection<ProductServiceModel> Products { get; set; }

        
    }
}
