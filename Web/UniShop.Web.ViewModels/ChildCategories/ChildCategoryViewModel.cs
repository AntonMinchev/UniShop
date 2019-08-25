using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Common;

namespace UniShop.Web.ViewModels.ChildCategories
{
    public class ChildCategoryViewModel : IMapFrom<ChildCategoryServiceModel>,IHaveCustomMappings
    {

        public int Id { get; set; }

        [Display(Name = ViewModelsConstants.Name)]
        public string Name { get; set; }

        [Display(Name = ViewModelsConstants.ParentCategoryName)]
        public string ParentCategoryName { get; set; }

        [Display(Name = ViewModelsConstants.CountOfProducts)]
        public int CountOfProducts { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<ChildCategoryServiceModel, ChildCategoryViewModel>()
                .ForMember(destination => destination.CountOfProducts,
                            opts => opts.MapFrom(origin => origin.Products.Count));
        }

    }
}
