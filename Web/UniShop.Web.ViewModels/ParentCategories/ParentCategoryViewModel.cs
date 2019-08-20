using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.ParentCategories
{
    public class ParentCategoryViewModel : IMapFrom<ParentCategoryServiceModel>,IHaveCustomMappings
    {
        [Display(Name = "Номер")]
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Брой подкатегории")]
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
