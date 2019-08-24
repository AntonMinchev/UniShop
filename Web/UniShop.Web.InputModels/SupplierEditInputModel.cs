using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniShop.Web.InputModels
{
    public class SupplierEditInputModel
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1} символа.")]
        public string Name { get; set; }

        [Display(Name = "Цена до офис")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "Полето \"{0}\" трябва да е число в диапазона от {1} до {2}")]
        public decimal PriceToOffice { get; set; }

        [Display(Name = "Цена до адрес")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "Полето \"{0}\" трябва да е число в диапазона от {1} до {2}")]
        public decimal PriceToHome { get; set; }
    }
}
