using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Web.Common;
using UniShop.Web.ViewModels.Orders;
using X.PagedList;

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
        public IActionResult UnprocessedOrders(int? pages)
        {
            var unprocessedOrders = this.orderService.GetAllUnprocessedOrders().To<UnprocessedOrderViewModel>().ToList();

            int pageNumber = pages ?? WebConstants.DefaultPageNumber;

            var pageUnprocrssedOrders = unprocessedOrders.ToPagedList(pageNumber, WebConstants.OrdersPageSize);

            return this.View(pageUnprocrssedOrders);
        }

        [HttpGet("/Administration/Orders/ProcessedOrders")]
        public IActionResult ProcessedOrders(int? pages)
        {
            var processedOrders = this.orderService.GetAllProcessedOrders().To<ProcessedOrderViewModel>().ToList();

            int pageNumber = pages ?? WebConstants.DefaultPageNumber;

            var pageProcrssedOrders = processedOrders.ToPagedList(pageNumber, WebConstants.OrdersPageSize);

            return this.View(pageProcrssedOrders);
        }

        [HttpGet("/Administration/Orders/DeliveredOrders")]
        public IActionResult DeliveredOrders(int? pages)
        {
            var deliveredOrders = this.orderService.GetAllDeliveredOrders().To<DeliveredOrderViewModel>().ToList();

            int pageNumber = pages ?? WebConstants.DefaultPageNumber;

            var pageDeliveredOrders = deliveredOrders.ToPagedList(pageNumber, WebConstants.OrdersPageSize);

            return this.View(pageDeliveredOrders);
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