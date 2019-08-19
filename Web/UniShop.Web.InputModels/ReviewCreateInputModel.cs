using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Services.Mapping;

namespace UniShop.Web.InputModels
{
    public class ReviewCreateInputModel 
    {
        public int Raiting { get; set; }

        public string Comment { get; set; }

        public int ProductId { get; set; }
    }
}
