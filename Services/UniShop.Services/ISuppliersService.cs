using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Services.Models;

namespace UniShop.Services
{
    public interface ISuppliersService
    {
        bool Create(SupplierServiceModel supplierServiceModel);

        IQueryable<SupplierServiceModel> GetAllSuppliers();
    }
}
