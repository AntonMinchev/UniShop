using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniShop.Data;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.ViewModels.Home;

namespace UniShop.Web.Controllers
{
    [Authorize]
    public class UniShopUsersController : BaseController
    {
        private readonly IUniShopUsersService uniShopUsersService;
        private readonly UniShopDbContext context;

        public UniShopUsersController(IUniShopUsersService uniShopUsersService,UniShopDbContext context)
        {
            this.uniShopUsersService = uniShopUsersService;
            this.context = context;
        }

       
    }
}