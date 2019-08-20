using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;

namespace UniShop.Web.InputModels
{
    public class ReviewCreateInputModel 
    {
        [Range(0, 5, ErrorMessage = "Количеството на продуктите  трябва да е число от {1} до {2}!")]
        public int Raiting { get; set; }

        [Display(Name ="Коментар")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително!")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public string Comment { get; set; }

        public int ProductId { get; set; }
    }
}
