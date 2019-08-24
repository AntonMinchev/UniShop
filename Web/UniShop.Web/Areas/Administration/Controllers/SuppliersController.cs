using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.InputModels;
using UniShop.Web.ViewModels;
using UniShop.Web.ViewModels.Suppliers;

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

        [HttpGet("/Administration/Suppliers/Edit")]
        public IActionResult Edit(int id)
        {
            var supplierEditInputModel = this.suppliersService.GetSupplierById(id).To<SupplierEditInputModel>();

            if (supplierEditInputModel == null)
            {
                return Redirect("All");
            }

            return this.View(supplierEditInputModel);
        }

        [HttpPost("/Administration/Suppliers/Edit")]
        public IActionResult Edit(SupplierEditInputModel supplierEditInputModel)
        {

            if (!ModelState.IsValid)
            {
                var supplierEditModel = this.suppliersService.GetSupplierById(supplierEditInputModel.Id).To<SupplierEditInputModel>();

                return this.View(supplierEditModel);
            }


            var supplierServiceModel = AutoMapper.Mapper.Map<SupplierServiceModel>(supplierEditInputModel);

            this.suppliersService.Edit(supplierServiceModel);

            return RedirectToAction("All");
        }

        [HttpGet("/Administration/Suppliers/Delete")]
        public IActionResult Delete(int id)
        {
           
            this.suppliersService.Delete(id);

            return Redirect("All");
        }
    }
}