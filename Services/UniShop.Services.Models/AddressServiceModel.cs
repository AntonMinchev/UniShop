using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;
using UniShop.Web.InputModels;

namespace UniShop.Services.Models
{
    public class AddressServiceModel : IMapFrom<Address>,IMapTo<Address>,IMapFrom<AddressCreateInputModel>
    {

        public int Id { get; set; }

        public string City { get; set; }
        //public CityServiceModel City { get; set; }

        public string UniShopUserId { get; set; }
        public UniShopUserServiceModel UniShopUser { get; set; }

        public string Street { get; set; }

        public string BuildingNumber { get; set; }

        public ICollection<OrderServiceModel> Orders { get; set; }
    }
}
