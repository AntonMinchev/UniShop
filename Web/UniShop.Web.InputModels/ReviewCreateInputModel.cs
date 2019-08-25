using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UniShop.Services.Mapping;
using UniShop.Web.InputModels.Common;

namespace UniShop.Web.InputModels
{
    public class ReviewCreateInputModel 
    {
        [Display(Name = InputModelsConstants.Raiting)]
        [Range(InputModelsConstants.RaitingMinNumber, InputModelsConstants.RaitingMaxNumber)]
        public int Raiting { get; set; }

        [Display(Name = InputModelsConstants.Comment )]
        [Required(ErrorMessage = InputModelsConstants.RequiredErrorMessage)]
        [StringLength(InputModelsConstants.CommentMaxLength, MinimumLength = InputModelsConstants.CommentMinLength, ErrorMessage = InputModelsConstants.StringLengthErrorMessage)]
        public string Comment { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}
