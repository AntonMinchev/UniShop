using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Data.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Postcode { get; set; }

        public ICollection<Address> Addresses { get; set; }

    }
}
