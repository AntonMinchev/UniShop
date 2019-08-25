using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniShop.Data.Models.Enums
{
    public enum DeliveryType
    {
        [Display(Name = "До адрес")]
        ToHome = 1,

        [Display(Name = "До офис на куриера")]
        ToOffice = 2
    }
}
