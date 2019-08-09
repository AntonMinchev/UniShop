using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Services
{
    public class UsersService : IUsersService
    {
        private readonly UniShopDbContext context;

        public UsersService(UniShopDbContext context)
        {
            this.context = context;
        }

        UniShopUserServiceModel IUsersService.GetUserByUsername(string username)
        {
            var user = this.context.Users.FirstOrDefault(u => u.UserName == username).To<UniShopUserServiceModel>();

            return user;
        }
    }
}
