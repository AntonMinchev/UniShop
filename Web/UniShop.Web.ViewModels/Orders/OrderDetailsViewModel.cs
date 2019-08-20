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
        public DateTime? EstimatedDeliveryDate { get; set; }

        [Display(Name = "Дата на доставак")]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name = "Дата на изпращане на продуктите")]
        public DateTime? DispatchDate { get; set; }

        [Display(Name = "Сума")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Цена на доставката")]
        public decimal DeliveryPrice { get; set; }

        [Display(Name = "Име на клиента")]
        public string RecipientFullName { get; set; }

        [Display(Name = "Телефонен номер")]
        public string RecipientPhoneNumber { get; set; }

        [Display(Name = "Адрес на клиента")]
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
