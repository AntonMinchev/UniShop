using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels
{
    public class ParentCategoryViewModel : IMapFrom<ParentCategoryServiceModel>,IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ChildCategoriesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<ParentCategoryServiceModel, ParentCategoryViewModel>()
                .ForMember(destination => destination.ChildCategoriesCount,
                            opts => opts.MapFrom(origin => origin.ChildCategories.Count));
        }
    }
}
