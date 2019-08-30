using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Common;

namespace UniShop.Web.ViewModels.Orders
{
    public class ProcessedOrderViewModel : IMapFrom<OrderServiceModel>,IHaveCustomMappings
    {
        [Display(Name = ViewModelsConstants.OrderId)]
        public int Id { get; set; }

        [Display(Name = ViewModelsConstants.OrderDate)]
        public DateTime OrderDate { get; set; }

        [Display(Name = ViewModelsConstants.EstimatedDeliveryDate)]
        public string EstimatedDeliveryDate { get; set; }

        [Display(Name = ViewModelsConstants.DispatchDate)]
        public DateTime DispatchDate { get; set; }

        [Display(Name = ViewModelsConstants.TotalPrice)]
        public decimal TotalPrice { get; set; }


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<OrderServiceModel, ProcessedOrderViewModel>()
                 .ForMember(destination => destination.EstimatedDeliveryDate,
                            opts => opts.MapFrom(origin => origin.EstimatedDeliveryDate.HasValue ?
                            origin.EstimatedDeliveryDate.Value.ToString("MM/dd/yyyy") : ""));
        }
    }
}
