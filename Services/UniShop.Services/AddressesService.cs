using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data;
using UniShop.Data.Models;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.InputModels;

namespace UniShop.Services
{

    public class AddressesService : IAddressesService
    {
        private readonly UniShopDbContext context;
        private readonly IUniShopUsersService uniShopUsersService;

        public AddressesService(UniShopDbContext context,IUniShopUsersService uniShopUsersService)
        {
            this.context = context;
            this.uniShopUsersService = uniShopUsersService;
        }

        public bool AddAddress(AddressServiceModel addressServiceModel, string userId)
        {

            Address address = new Address
            {               
               UniShopUserId = userId,
                City = addressServiceModel.City,
                Street = addressServiceModel.Street,
                BuildingNumber = addressServiceModel.BuildingNumber
            };

            this.context.Addresses.Add(address);
            int result = this.context.SaveChanges();

            

            return result > 0;
        }

        public IQueryable<AddressServiceModel> GetAddressesByUserName(string username)
        {
            var addresses = this.context.Addresses.Where(a => a.UniShopUser.UserName == username).To<AddressServiceModel>();

            return addresses;
        }

      
    }
}
