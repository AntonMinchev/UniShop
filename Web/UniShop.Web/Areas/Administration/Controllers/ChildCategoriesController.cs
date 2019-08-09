using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.InputModels;
using UniShop.Web.ViewModels;
using UniShop.Web.ViewModels.ChildCategories;

namespace UniShop.Web.Areas.Administration.Controllers
{
    public class ChildCategoriesController : AdminController
    {
        private readonly IChildCategoriesService childCategoriesService;
        private readonly IParentCategoriesService parentCategoriesService;

        public ChildCategoriesController(IChildCategoriesService childCategoriesService,IParentCategoriesService parentCategoriesService)
        {
            this.childCategoriesService = childCategoriesService;
            this.parentCategoriesService = parentCategoriesService;
        }

        [HttpGet("/Administration/ChildCategories/Create")]
        public IActionResult Create()
        {
            var parentCategories = this.parentCategoriesService.GetAllParentCategories();

            this.ViewData["types"] = parentCategories.Select(parentCategory => new ChildCategoryCreateParentCategoryViewModel
            {
                Id = parentCategory.Id,
                Name = parentCategory.Name
            })
                .ToList(); ;

            return this.View();
        }

        [HttpPost("/Administration/ChildCategories/Create")]
        public IActionResult Create(ChildCategoryCreateInputModel childCategoryCreateInputModel)
        {
            ChildCategoryServiceModel childCategoryServiceModel = childCategoryCreateInputModel.To<ChildCategoryServiceModel>();

            this.childCategoriesService.Create(childCategoryServiceModel);

            return Redirect("/");
        }

        [HttpGet("/Administration/ChildCategories/All")]
        public IActionResult All()
        {
            var childCategoryViewModels = this.childCategoriesService.GetAllChildCategories().To<ChildCategoryViewModel>().ToList();

            return this.View(childCategoryViewModels);
        }
    }
}