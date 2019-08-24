using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniShop.Web.InputModels
{
    public class ProductEditInputModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Име")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1} символа.")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително!")]
        [Range(typeof(decimal), "0.1", "79228162514264337593543950335", ErrorMessage = "Цената трябва да е между {1} и {2}!")]
        public decimal Price { get; set; }

        [Display(Name = "Количество")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително!")]
        [Range(1, int.MaxValue, ErrorMessage = "Количеството на продуктите  трябва да е число от {1} до {2}!")]
        public int Quantity { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително!")]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1} символа.")]
        public string Description { get; set; }

        [Display(Name = "Характеристики")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително!")]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1} символа.")]
        public string Specification { get; set; }

        [Display(Name = "Снимка")]
        public IFormFile Image { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително!")]
        public string ChildCategoryName { get; set; }
    }
}
