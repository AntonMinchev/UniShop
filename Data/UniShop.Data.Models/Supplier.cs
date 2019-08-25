using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Data.Models
{
    public class Supplier
    {
        public Supplier()
        {
            this.Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal PriceToHome { get; set; }

        public decimal PriceToOffice { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
