using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using UniShop.Data.Models;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Home
{
    public class ProductHomeViewModel : IMapFrom<ProductServiceModel>,IMapFrom<Product>,IHaveCustomMappings
    {
     
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public int Raiting { get; set; }

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
