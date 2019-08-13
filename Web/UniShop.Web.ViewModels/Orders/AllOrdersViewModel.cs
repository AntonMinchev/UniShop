using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models.Enums;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Orders
{
    public class AllOrdersViewModel : IMapFrom<OrderServiceModel>
    {
        public int Id { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime? DispatchDate { get; set; }

        public decimal TotalPrice { get; set; }

    }
}
