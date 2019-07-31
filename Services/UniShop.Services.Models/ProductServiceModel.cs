using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;
using UniShop.Web.InputModels;

namespace UniShop.Services.Models
{
    public class ProductServiceModel : IMapFrom<Product>, IMapTo<Product>
        ,IMapFrom<ProductCreateInputModel> ,IMapTo<ProductCreateInputModel>,IHaveCustomMappings
    {
        

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int ChildCategoryId { get; set; }

        public ChildCategoryServiceModel ChildCategory { get; set; }

        public string ChildCategoryName { get; set; }

        public string Image { get; set; }

        public int Raiting { get; set; }

        public int CountReviews { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Product, ProductServiceModel>()
                .ForMember(destination => destination.Raiting,
                            opts => opts.MapFrom(origin => origin.Reviews.Select(x=>x.Raiting).Sum()))
                .ForMember(destination => destination.CountReviews,
                            opts => opts.MapFrom(origin => origin.Reviews.Count()));
        }
    }
}
