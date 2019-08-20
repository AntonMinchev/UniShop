using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Products
{
    public class ProductReviewViewModel : IMapFrom<ReviewServiceModel>,IHaveCustomMappings
    {
        public int id { get; set; }

        public string Username { get; set; }

        public int Raiting { get; set; }

        public string Comment { get; set; }

        public int ProductId { get; set; }



        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
               .CreateMap<ReviewServiceModel, ProductReviewViewModel>()
               .ForMember(destination => destination.Username,
                           opts => opts.MapFrom(origin => origin.UniShopUser.UserName));
        }
    }
}
