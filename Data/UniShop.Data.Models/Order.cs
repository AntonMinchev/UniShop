﻿using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models.Enums;

namespace UniShop.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime? DispatchDate { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public string Recipient { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public string SupplierName { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public string UniShopUserId { get; set; }
        public UniShopUser UniShopUser { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts  { get; set; }

        public int? DeliveryAddressId { get; set; }
        public virtual Address DeliveryAddress { get; set; }

    }
}
