using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Orders
{
    public class ProcessedOrderViewModel : IMapFrom<OrderServiceModel>
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        public DateTime DispatchDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
