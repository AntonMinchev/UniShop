using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;

namespace UniShop.Services.Models
{
    public class ShoppingCartServiceModel : IMapFrom<ShoppingCart>,IMapTo<ShoppingCart>
    {
        public int Id { get; set; }

        public UniShopUserServiceModel User { get; set; }

        public virtual ICollection<ShoppingCartProductServiceModel> ShoppingCartProducts { get; set; }
    }
}
