using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Data.Models.Enums;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Addresses;

namespace UniShop.Web.ViewModels.Orders
{
    public class OrderDetailsViewModel : IMapFrom<OrderServiceModel>,IHaveCustomMappings
    {
        [Display(Name = "Номер на поръчка")]
        public int Id { get; set; }

        [Display(Name = "Статус")]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = "Дата на поръчка")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Очаквана дата за доставка")]
        public string EstimatedDeliveryDate { get; set; }

        [Display(Name = "Дата на доставак")]
        public string DeliveryDate { get; set; }

        [Display(Name = "Дата на изпращане на продуктите")]
        public string DispatchDate { get; set; }

        [Display(Name = "Сума")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Цена на доставката")]
        public decimal DeliveryPrice { get; set; }

        [Display(Name = "Име на клиента")]
        public string Recipient { get; set; }

        [Display(Name = "Телефонен номер")]
        public string RecipientPhoneNumber { get; set; }

        [Display(Name = "Адрес на клиента")]
        public string Address { get; set; }

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
