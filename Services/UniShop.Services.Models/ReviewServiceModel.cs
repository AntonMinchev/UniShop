using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;

namespace UniShop.Services.Models
{
    public class ReviewServiceModel : IMapFrom<Review>,IMapTo<Review>
    {
        public int Id { get; set; }

        public int Raiting { get; set; }

        public string Comment { get; set; }

        public int ProductId { get; set; }
        public ProductServiceModel Product { get; set; }
    }
}
