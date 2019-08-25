using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniShop.Web.InputModels
{
    public class OrderCreateInputModel
    {
        [Required]
        public int AddressId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public string DeliveryType { get; set; }
    
    }
}
