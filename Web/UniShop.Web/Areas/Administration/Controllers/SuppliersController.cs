using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.InputModels;
using UniShop.Web.ViewModels;

namespace UniShop.Web.Areas.Administration.Controllers
{
    public class SuppliersController : AdminController
    {
        private readonly ISuppliersService suppliersService;

        public SuppliersController(ISuppliersService suppliersService)
        {
            this.suppliersService = suppliersService;
        }

        [HttpGet("/Administration/Suppliers/Create")]
        public IActionResult Create()
        {
            return this.View("Create");
        }


        [HttpPost("/Administration/Suppliers/Create")]
        public IActionResult Create(SupplierCreateInputModel supplierCreateInputModel)
        {
            SupplierServiceModel supplierServiceModel = supplierCreateInputModel.To<SupplierServiceModel>();

            this.suppliersService.Create(supplierServiceModel);

            return Redirect("/");
        }

        [HttpGet("/Administration/Suppliers/All")]
        public IActionResult All()
        {
            var suppliersViewModels = this.suppliersService.GetAllSuppliers().To<SupplierViewModel>().ToList();

            return this.View(suppliersViewModels);
        }

    }
}