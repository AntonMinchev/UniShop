using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Web.InputModels
{
    public class OrderCreateInputModel
    {
        public int AddressId { get; set; }

        public int SupplierId { get; set; }

        public string DeliveryType { get; set; }
    
    }
}
