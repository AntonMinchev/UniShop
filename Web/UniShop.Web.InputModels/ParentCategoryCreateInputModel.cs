﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;

namespace UniShop.Web.InputModels
{
    public class ParentCategoryCreateInputModel
    {
        [Display(Name = "Име")]
        [Required(ErrorMessage ="Полето е задължително!")]
        public string Name { get; set; }
    }
}
