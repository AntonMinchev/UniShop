using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;
using UniShop.Web.InputModels;

namespace UniShop.Services.Models
{
    public class ChildCategoryServiceModel : IMapFrom<ChildCategoryCreateInputModel>,IMapFrom<ChildCategory>,IHaveCustomMappings
    {
       
        public string Name { get; set; }

        public string ParentCategoryName { get; set; }

        public int ParentCategoryId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<ChildCategory, ChildCategoryServiceModel>()
                .ForMember(destination => destination.ParentCategoryName,
                            opts => opts.MapFrom(origin => origin.ParentCategory.Name));
        }
    }
}
