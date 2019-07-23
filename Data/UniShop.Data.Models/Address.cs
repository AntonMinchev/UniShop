using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Data.Models
{
    public class Address
    {
        public int Id { get; set; }

        public int CityId { get; set; }
        public City City  { get; set; }

        public string UniShopUserId { get; set; }
        public UniShopUser UniShopUser { get; set; }

        public string Street { get; set; }

        public string BuildingNumber { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
