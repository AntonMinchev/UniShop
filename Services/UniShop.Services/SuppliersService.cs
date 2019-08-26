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
    public class SuppliersService : ISuppliersService
    {
        private readonly UniShopDbContext context;

        public SuppliersService(UniShopDbContext context)
        {
            this.context = context;
        }

      
        public bool Create(SupplierServiceModel supplierServiceModel)
        {
            Supplier supplier = new Supplier
            {
                Name = supplierServiceModel.Name,
                PriceToHome = supplierServiceModel.PriceToHome,
                PriceToOffice = supplierServiceModel.PriceToOffice
            };

            this.context.Suppliers.Add(supplier);

            int result = this.context.SaveChanges();

            return result > 0;
        }

        public bool Delete(int id)
        {
            var supplier = this.context.Suppliers.FirstOrDefault(s => s.Id == id);

            if (supplier == null)
            {
                return false;
            }

            this.context.Remove(supplier);
            int result = this.context.SaveChanges();

            return result > 0;
        }

        public bool Edit(SupplierServiceModel supplierEditInputModel)
        {
            var supplier = this.context.Suppliers.FirstOrDefault(s => s.Id == supplierEditInputModel.Id);

            if (supplier == null)
            {
                return false;
            }

            supplier.Name = supplierEditInputModel.Name;
            supplier.PriceToHome = supplierEditInputModel.PriceToHome;
            supplier.PriceToOffice = supplierEditInputModel.PriceToOffice;

            this.context.Update(supplier);
            int result = this.context.SaveChanges();

            return result > 0;

        }

        public IQueryable<SupplierServiceModel> GetAllSuppliers()
        {
            var suppliers = this.context.Suppliers.To<SupplierServiceModel>();

            return suppliers;
        }

        public SupplierServiceModel GetSupplierById(int id)
        {
            var supplier = this.context.Suppliers.To<SupplierServiceModel>().FirstOrDefault(s => s.Id == id);

            return supplier;
        }


    }
}
