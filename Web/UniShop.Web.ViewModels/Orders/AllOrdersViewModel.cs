using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Data.Models.Enums;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Common;

namespace UniShop.Web.ViewModels.Orders
{
    public class AllOrdersViewModel : IMapFrom<OrderServiceModel>,IHaveCustomMappings
    {
        [Display(Name = ViewModelsConstants.OrderId)]
        public int Id { get; set; }

        [Display(Name = ViewModelsConstants.OrderStatus)]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = ViewModelsConstants.OrderDate)]
        public DateTime OrderDate { get; set; }

        [Display(Name = ViewModelsConstants.EstimatedDeliveryDate)]
        public string EstimatedDeliveryDate { get; set; }

        [Display(Name = ViewModelsConstants.DeliveryDate)]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name = ViewModelsConstants.DispatchDate)]
        public DateTime? DispatchDate { get; set; }

        [Display(Name = ViewModelsConstants.TotalPrice)]
        public decimal TotalPrice { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<OrderServiceModel, AllOrdersViewModel>()
                .ForMember(destination => destination.EstimatedDeliveryDate,
                            opts => opts.MapFrom(origin => origin.EstimatedDeliveryDate.HasValue? 
                            origin.EstimatedDeliveryDate.Value.ToString("MM/dd/yyyy"):""));
        }
    }
}
