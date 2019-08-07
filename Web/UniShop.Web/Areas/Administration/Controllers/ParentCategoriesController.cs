using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.InputModels;
using UniShop.Web.ViewModels;

namespace UniShop.Web.Areas.Administration.Controllers
{
    public class ParentCategoriesController : AdminController
    {
        private readonly IParentCategoriesService parentCategoriesService;

        public ParentCategoriesController(IParentCategoriesService parentCategoriesService)
        {
            this.parentCategoriesService = parentCategoriesService;
        }

        [HttpGet("/Administration/ParentCategories/Create")]
        public IActionResult Create()
        {
            return this.View("Create");
        }



        [HttpPost("/Administration/ParentCategories/Create")]
        public IActionResult Create(ParentCategoryCreateInputModel parentCategoryCreateInput )
        {
            ParentCategoryServiceModel parentCategoryServiceModel = new ParentCategoryServiceModel
            {
                Name = parentCategoryCreateInput.Name
            };

            this.parentCategoriesService.Create(parentCategoryServiceModel);

            return this.Redirect("/");
        }

        [HttpGet("/Administration/ParentCategories/All")]
        public IActionResult All()
        {
            var parentCategoryViewModels = this.parentCategoriesService.GetAllParentCategories().To<ParentCategoryViewModel>().ToList();

            return this.View(parentCategoryViewModels);
        }
    }
}