using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Models;

namespace UniShop.Services.Contracts
{
    public interface IUsersService
    {
        UniShopUserServiceModel GetUserByUsername(string username);
    }
}
