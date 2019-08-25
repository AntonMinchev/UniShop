using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Data.Models.Enums;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Addresses;
using UniShop.Web.ViewModels.Common;

namespace UniShop.Web.ViewModels.Orders
{
    public class OrderDetailsViewModel : IMapFrom<OrderServiceModel>,IHaveCustomMappings
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
        public string DeliveryDate { get; set; }

        [Display(Name = ViewModelsConstants.DispatchDate)]
        public string DispatchDate { get; set; }

        [Display(Name = ViewModelsConstants.TotalPrice)]
        public decimal TotalPrice { get; set; }

        [Display(Name = ViewModelsConstants.DeliveryPrice)]
        public decimal DeliveryPrice { get; set; }

        [Display(Name = ViewModelsConstants.Recipient)]
        public string Recipient { get; set; }

        [Display(Name = ViewModelsConstants.RecipientPhoneNumber)]
        public string RecipientPhoneNumber { get; set; }

        [Display(Name = ViewModelsConstants.Address)]
        public string Address { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public string SupplierName { get; set; }

        public ICollection<OrderProductViewModel> OrderProducts { get; set; }


        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration
        //        .CreateMap<OrderServiceModel, DeliveredOrderViewModel>()
        //        .ForMember(destination => destination.EstimatedDeliveryDate,
        //                    opts => opts.MapFrom(origin => origin.EstimatedDeliveryDate.HasValue ?
        //                    origin.EstimatedDeliveryDate.Value.ToString("MM/dd/yyyy") : ""))
        //         .ForMember(destination => destination.DeliveryDate,
        //                    opts => opts.MapFrom(origin => origin.DeliveryDate.HasValue ?
        //                    origin.EstimatedDeliveryDate.Value.ToString("MM/dd/yyyy") : ""));
        //}

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<OrderServiceModel, OrderDetailsViewModel>()
                .ForMember(destination => destination.Address,
                            opts => opts.MapFrom(origin => "гр." +origin.DeliveryAddress.City + " ул." +origin.DeliveryAddress.Street + " " +origin.DeliveryAddress.BuildingNumber))
                .ForMember(destination => destination.EstimatedDeliveryDate,
                           opts => opts.MapFrom(origin => origin.EstimatedDeliveryDate.HasValue ?
                           origin.EstimatedDeliveryDate.Value.ToString("MM/dd/yyyy") : ""))
                .ForMember(destination => destination.DeliveryDate,
                           opts => opts.MapFrom(origin => origin.DeliveryDate.HasValue ?
                           origin.DeliveryDate.Value.ToString("MM/dd/yyyy") : ""));
        }
    }
}
