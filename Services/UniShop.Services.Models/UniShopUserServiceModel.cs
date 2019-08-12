using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;

namespace UniShop.Services.Models
{
    public class UniShopUserServiceModel : IdentityUser<string> ,IMapTo<UniShopUser>,IMapFrom<UniShopUser>,IHaveCustomMappings
    {
        public string FullName { get; set; }

        public ICollection<AddressServiceModel> Addresses { get; set; }

        public ICollection<OrderServiceModel> Orders { get; set; }


        public int ShoppingCartId { get; set; }
        public ShoppingCartServiceModel ShoppingCart { get; set; }

        public virtual ICollection<UniShopUserFavoriteProductServiceModel> FavoriteProducts { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
               .CreateMap<UniShopUser, UniShopUserServiceModel>()
               .ForMember(destination => destination.Addresses,
                           opts => opts.MapFrom(origin => origin.Addresses));
        }
    }
}
