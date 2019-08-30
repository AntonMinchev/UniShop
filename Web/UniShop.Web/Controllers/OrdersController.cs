using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Web.Common;
using UniShop.Web.InputModels;
using UniShop.Web.ViewModels.Addresses;
using UniShop.Web.ViewModels.Orders;
using UniShop.Web.ViewModels.Products;
using UniShop.Web.ViewModels.ShoppingCarts;
using UniShop.Web.ViewModels.Suppliers;
using X.PagedList;

namespace UniShop.Web.Controllers
{
    [Authorize]
    public class OrdersController :  BaseController
    {
        private readonly ISuppliersService suppliersService;
        private readonly IAddressesService addressesService;
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly IUniShopUsersService uniShopUsersService;
        private readonly IOrderService orderService;

        public OrdersController(ISuppliersService suppliersService,
            IAddressesService addressesService,IShoppingCartsService shoppingCartsService
            ,IUniShopUsersService uniShopUsersService,IOrderService orderService)
        {
            this.suppliersService = suppliersService;
            this.addressesService = addressesService;
            this.shoppingCartsService = shoppingCartsService;
            this.uniShopUsersService = uniShopUsersService;
            this.orderService = orderService;
        }

        [HttpGet]
        public IActionResult Create()
        {

            string username = this.User.FindFirst(ClaimTypes.Name).Value;
            
            var shoppingCartProducts = this.shoppingCartsService.GetAllShoppingCartProducts(username).ToList();

            var shoppingCartProductsViewModel = shoppingCartProducts.Select(product => new ShoppingCartProductViewModel
            {
                Id = product.ProductId,
                Product = product.Product.To<ProductViewModel>(),
                Quantity = product.Quantity,
                TotalPrice = product.Quantity * product.Product.Price

            }).ToList();

            var user = this.uniShopUsersService.GetUserByUsername(username);

            var suppliers = this.suppliersService.GetAllSuppliers().To<SupplierViewModel>().ToList();

            var addreses = this.addressesService.GetAddressesByUserName(username).To<AddressViewModel>().ToList();

            this.ViewData["addresses"] = addreses;
            this.ViewData["fullname"] = user.FullName;
            this.ViewData["phoneNumber"] = user.PhoneNumber;
            this.ViewData["suppliers"] = suppliers;
            this.ViewData["products"] = shoppingCartProductsViewModel;


            return View();
        }


        [HttpPost]
        public IActionResult Create(OrderCreateInputModel orderCreateInputModel)
        {
           
            string username = this.User.FindFirst(ClaimTypes.Name).Value;

            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("Create");
            }


            var isCreated = this.orderService.CreateOrder(username, orderCreateInputModel.SupplierId, orderCreateInputModel.DeliveryType, orderCreateInputModel.AddressId);

            if (!isCreated)
            {
                this.TempData["error"] = WebConstants.ErrorOrderMessage;
                return this.RedirectToAction("Create");
            }

            return this.RedirectToAction("Successfully");
        }

        public IActionResult All(int? pages)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var orders = this.orderService.GetAllOrdersByUserId(userId).To<AllOrdersViewModel>().ToList();

            int pageNumber = pages ?? WebConstants.DefaultPageNumber;

            var pageOrders = orders.ToPagedList(pageNumber, WebConstants.OrdersPageSize);

            return this.View(pageOrders);
        }

        public IActionResult Details(int id)
        {

            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var order = this.orderService.GetOrderByIdAndUserId(id,userId);

            if (order == null)
            {
                return RedirectToAction("All");
            }

            var orderDetailsViewModel = order.To<OrderDetailsViewModel>();
            return this.View(orderDetailsViewModel);
        }

        public IActionResult Successfully()
        {
            return this.View();
        }
    }
}