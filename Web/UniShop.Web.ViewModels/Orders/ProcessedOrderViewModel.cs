using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Orders
{
    public class ProcessedOrderViewModel : IMapFrom<OrderServiceModel>
    {
        [Display(Name = "Номер на поръчка")]
        public int Id { get; set; }

        [Display(Name = "Дата на поръчка")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Очаквана дата на доставка")]
        public DateTime EstimatedDeliveryDate { get; set; }

        [Display(Name = "Дата на изпращане")]
        public DateTime DispatchDate { get; set; }

        [Display(Name = "Сума")]
        public decimal TotalPrice { get; set; }
    }
}
