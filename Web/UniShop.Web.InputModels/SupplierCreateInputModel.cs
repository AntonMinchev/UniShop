using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Web.InputModels
{
    public class SupplierCreateInputModel
    {
        public string Name { get; set; }

        public decimal PriceToOffice { get; set; }

        public decimal PriceToAddress { get; set; }
    }
}
