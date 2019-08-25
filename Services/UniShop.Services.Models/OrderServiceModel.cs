using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Data.Models.Enums;
using UniShop.Services.Mapping;

namespace UniShop.Services.Models
{
    public class OrderServiceModel : IMapFrom<Order>,IMapTo<Order>
    {
        public int Id { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime? DispatchDate { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public string SupplierName { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public string Recipient { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public string UniShopUserId { get; set; }
        public UniShopUserServiceModel UniShopUser { get; set; }

        public ICollection<OrderProductServiceModel> OrderProducts { get; set; }

        public int? DeliveryAddressId { get; set; }
        public AddressServiceModel DeliveryAddress { get; set; }
    }
}
