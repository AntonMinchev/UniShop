using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Web.ViewModels.Orders;

namespace UniShop.Web.Areas.Administration.Controllers
{
    public class OrdersController : AdminController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("/Administration/Orders/UnprocessedOrders")]
        public IActionResult UnprocessedOrders()
        {
            var unprocessedOrders = this.orderService.GetAllUnprocessedOrders().To<UnprocessedOrderViewModel>().ToList();

            return this.View(unprocessedOrders);
        }

        [HttpGet("/Administration/Orders/ProcessedOrders")]
        public IActionResult ProcessedOrders()
        {
            var processedOrders = this.orderService.GetAllProcessedOrders().To<ProcessedOrderViewModel>().ToList();

            return this.View(processedOrders);
        }

        [HttpGet("/Administration/Orders/DeliveredOrders")]
        public IActionResult DeliveredOrders()
        {
            var deliveredOrders = this.orderService.GetAllDeliveredOrders().To<DeliveredOrderViewModel>().ToList();

            return this.View(deliveredOrders);
        }



        public IActionResult Processing(int id)
        {
            this.orderService.ProcessingOrder(id);

            return this.Redirect("/Administration/Orders/ProcessedOrders");
        }


        public IActionResult Deliver(int id)
        {
            this.orderService.DeliverOrder(id);

            return this.Redirect("/Administration/Orders/DeliveredOrders");
        }
    }
}