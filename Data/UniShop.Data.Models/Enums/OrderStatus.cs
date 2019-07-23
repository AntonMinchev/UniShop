using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniShop.Data.Models.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Необработена")]
        Unprocessed = 1,

        [Display(Name = "Обработена")]
        Processed = 2,

        [Display(Name = "Доставена")]
        Delivered = 3
    }
}
