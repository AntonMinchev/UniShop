using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;

namespace UniShop.Web.InputModels
{
    public class AddressCreateInputModel 
    {
        [Display(Name ="Град")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public string City { get; set; }

        [Display(Name = "Адрес")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public string Street { get; set; }

        [Display(Name = "Номер на сградата")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public string BuildingNumber { get; set; }
    }
}
