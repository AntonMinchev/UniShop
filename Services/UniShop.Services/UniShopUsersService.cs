﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Services
{
    public class UniShopUsersService : IUniShopUsersService
    {
        private readonly UniShopDbContext context;

        public UniShopUsersService(UniShopDbContext context)
        {
            this.context = context;
        }

        public UniShopUserServiceModel GetUserById(string Id)
        {
            var user = this.context.Users.To<UniShopUserServiceModel>().FirstOrDefault(u => u.Id == Id);

            return user;
        }

        UniShopUserServiceModel IUniShopUsersService.GetUserByUsername(string username)
        {
            var user = this.context.Users.To<UniShopUserServiceModel>().FirstOrDefault(u => u.UserName == username);

            return user;
        }
    }
}
