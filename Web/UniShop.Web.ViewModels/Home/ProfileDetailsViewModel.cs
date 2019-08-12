using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.Home
{
    public class ProfileDetailsViewModel : IMapFrom<UniShopUserServiceModel>
    {
        public string Username { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<AddressServiceModel> Addresses { get; set; }
    }
}
