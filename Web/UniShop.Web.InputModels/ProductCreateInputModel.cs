using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;

namespace UniShop.Web.InputModels
{
    public class ProductCreateInputModel 
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Specification { get; set; }
        
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string ChildCategoryName { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration
        //        .CreateMap<ProductCreateInputModel, ProductServiceModel>()
        //        .ForMember(destination => destination.ProductType,
        //                    opts => opts.MapFrom(origin => new ProductTypeServiceModel { Name = origin.ProductType }));
        //}
    }
}
