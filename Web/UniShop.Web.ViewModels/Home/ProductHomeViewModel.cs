using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using AutoMapper;
using UniShop.Data.Models;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Common;

namespace UniShop.Web.ViewModels.Home
{
    public class ProductHomeViewModel : IMapFrom<ProductServiceModel>,IMapFrom<Product>,IHaveCustomMappings
    {
     
        public string Id { get; set; }

        [Display(Name = ViewModelsConstants.Name)]
        public string Name { get; set; }

        [Display(Name = ViewModelsConstants.Price)]
        public decimal Price { get; set; }

        [Display(Name = ViewModelsConstants.Image)]
        public string Image { get; set; }

        [Display(Name = ViewModelsConstants.Raiting)]
        public int Raiting { get; set; }

        [Display(Name = ViewModelsConstants.CountReviews)]
        public int CountReviews { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
              .CreateMap<ProductServiceModel, ProductHomeViewModel>()
              .ForMember(destination => destination.CountReviews,
                          opts => opts.MapFrom(origin => origin.Reviews.Count))
              .ForMember(destination => destination.Raiting,
                          opts => opts.MapFrom(origin => origin.Reviews.Sum(x=>x.Raiting)));
        }
    }
}
