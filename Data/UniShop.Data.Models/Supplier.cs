using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Data.Models
{
    public class Supplier
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal PriceToHome { get; set; }

        public decimal PriceToOffice { get; set; }

    }
}
