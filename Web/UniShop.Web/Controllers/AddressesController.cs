using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.InputModels;

namespace UniShop.Web.Controllers
{
    [Authorize]
    public class AddressesController : BaseController
    {
        private readonly IAddressesService addressesService;

        public AddressesController(IAddressesService addressesService)
        {
            this.addressesService = addressesService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddressCreateInputModel addressCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            AddressServiceModel addressServiceModel = addressCreateInputModel.To<AddressServiceModel>();

            this.addressesService.AddAddress(addressServiceModel, userId);

            return this.Redirect("/Orders/Create");

        }
    }
}