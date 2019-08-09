using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.ShoppingCarts
{
    public class ShoppingCartProductViewModel : IMapFrom<ShoppingCartProductServiceModel>
    {
        public int Id { get; set; }

        public ProductServiceModel Product { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<ShoppingCartProductServiceModel, ShoppingCartProductViewModel>()
                .ForMember(destination => destination.TotalPrice,
                            opts => opts.MapFrom(origin => origin.Product.Price * origin.Quantity));
        }
    }
}
