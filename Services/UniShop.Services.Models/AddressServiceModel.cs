using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;

namespace UniShop.Services.Models
{
    public class AddressServiceModel : IMapFrom<Address>,IMapTo<Address>
    {

        public int Id { get; set; }

        public int CityId { get; set; }
        public CityServiceModel City { get; set; }

        public string UniShopUserId { get; set; }
        public UniShopUserServiceModel UniShopUser { get; set; }

        public string Street { get; set; }

        public string BuildingNumber { get; set; }

        public ICollection<OrderServiceModel> Orders { get; set; }
    }
}
