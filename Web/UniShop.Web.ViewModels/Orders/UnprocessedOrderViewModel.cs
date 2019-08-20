using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Orders
{
    public class UnprocessedOrderViewModel : IMapFrom<OrderServiceModel>
    {
        [Display(Name ="Номер на поръчка")]
        public int Id { get; set; }

        [Display(Name = "Дата на поръчката")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Очаквана дата за доставка")]
        public DateTime EstimatedDeliveryDate { get; set; }

        [Display(Name = "Сума")]
        public decimal TotalPrice { get; set; }
    }
}
