using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Common;

namespace UniShop.Web.ViewModels.ParentCategories
{
    public class ParentCategoryViewModel : IMapFrom<ParentCategoryServiceModel>,IHaveCustomMappings
    {
        [Display(Name = ViewModelsConstants.ParentCategoryId)]
        public int Id { get; set; }

        [Display(Name = ViewModelsConstants.Name)]
        public string Name { get; set; }

        [Display(Name = ViewModelsConstants.ChildCategoriesCount)]
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
