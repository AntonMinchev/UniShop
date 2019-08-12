using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Models;

namespace UniShop.Services.Contracts
{
    public interface IUniShopUsersService
    {
        UniShopUserServiceModel GetUserByUsername(string username);

        UniShopUserServiceModel GetUserById(string Id);
    }
}
