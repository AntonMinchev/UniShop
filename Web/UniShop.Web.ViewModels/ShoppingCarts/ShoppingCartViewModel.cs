using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.ShoppingCarts
{
    public class ShoppingCartViewModel : IMapFrom<ShoppingCartProductServiceModel>
    {
       
        public ProductServiceModel Product { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration
        //        .CreateMap<ShoppingCartProductServiceModel, ShoppingCartViewModel>()
        //        .ForMember(destination => destination.TotalPrice,
        //                    opts => opts.MapFrom(origin => origin.Product.Price * origin.Quantity))
        //        .ForMember(destination => destination.Id,
        //                    opts => opts.MapFrom(origin => origin.ProductId))
        //        .ForMember(destination => destination.Name,
        //                    opts => opts.MapFrom(origin => origin.Product.Name))
        //        .ForMember(destination => destination.Image,
        //                    opts => opts.MapFrom(origin => origin.Product.Image))
        //        .ForMember(destination => destination.Price,
        //                    opts => opts.MapFrom(origin => origin.Product.Price))
        //        .ForMember(destination => destination.Quantity,
        //                    opts => opts.MapFrom(origin => origin.Quantity));
        //}
    }
}
