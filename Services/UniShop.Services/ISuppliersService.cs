using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Models;

namespace UniShop.Services
{
    public interface ISuppliersService
    {
        bool Create(SupplierServiceModel supplierServiceModel);
    }
}
