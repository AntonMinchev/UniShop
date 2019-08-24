using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Orders
{
    public class UnprocessedOrderViewModel : IMapFrom<OrderServiceModel>,IHaveCustomMappings
    {
        [Display(Name ="Номер на поръчка")]
        public int Id { get; set; }

        [Display(Name = "Дата на поръчката")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Очаквана дата за доставка")]
        public string EstimatedDeliveryDate { get; set; }

        [Display(Name = "Сума")]
        public decimal TotalPrice { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<OrderServiceModel, UnprocessedOrderViewModel>()
                 .ForMember(destination => destination.EstimatedDeliveryDate,
                            opts => opts.MapFrom(origin => origin.EstimatedDeliveryDate.HasValue ?
                            origin.EstimatedDeliveryDate.Value.ToString("MM/dd/yyyy") : ""));
        }
    }
}
