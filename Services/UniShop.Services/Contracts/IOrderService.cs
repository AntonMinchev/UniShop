using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Services.Contracts
{
    public interface IOrderService
    {
        bool CreateOrder(string username, int supplierId, string deliveryType,int addressId);
    }
}
