using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Services.Models;

namespace UniShop.Services.Contracts
{
    public interface IOrderService
    {
        bool CreateOrder(string username, int supplierId, int deliveryType,int addressId);

        IQueryable<OrderServiceModel> GetAllUnprocessedOrders();

        IQueryable<OrderServiceModel> GetAllProcessedOrders();

        IQueryable<OrderServiceModel> GetAllDeliveredOrders();

        bool ProcessingOrder(int id);

        bool DeliverOrder(int id);

        OrderServiceModel GetOrderById(int id);

        IQueryable<OrderServiceModel> GetAllOrdersByUserId(string userId);
    }
}
