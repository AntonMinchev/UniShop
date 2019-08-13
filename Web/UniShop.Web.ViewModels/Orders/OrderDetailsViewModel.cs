using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models.Enums;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Addresses;

namespace UniShop.Web.ViewModels.Orders
{
    public class OrderDetailsViewModel : IMapFrom<OrderServiceModel>,IHaveCustomMappings
    {
        public int Id { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime? DispatchDate { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public string RecipientFullName { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public AddressViewModel Address { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<OrderServiceModel, OrderDetailsViewModel>()
                .ForMember(destination => destination.RecipientFullName,
                            opts => opts.MapFrom(origin => origin.UniShopUser.FullName))
                 .ForMember(destination => destination.RecipientPhoneNumber,
                            opts => opts.MapFrom(origin => origin.UniShopUser.PhoneNumber));


        }
    }
}
