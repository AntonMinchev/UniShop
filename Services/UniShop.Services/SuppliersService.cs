using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data;
using UniShop.Data.Models;
using UniShop.Services.Models;

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
                PriceToHome = supplierServiceModel.PriceToAddress,
                PriceToOffice = supplierServiceModel.PriceToOffice
            };

            this.context.Suppliers.Add(supplier);

            int result = this.context.SaveChanges();

            return result > 0;
        }
    }
}
