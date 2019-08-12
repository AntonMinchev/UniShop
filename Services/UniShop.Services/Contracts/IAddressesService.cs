using System.Linq;
using UniShop.Services.Models;

namespace UniShop.Services.Contracts
{
    public interface IAddressesService
    {
        bool AddAddress(AddressServiceModel addressServiceModel , string userId);

        IQueryable<AddressServiceModel> GetAddressesByUserName(string username);
    }
}
