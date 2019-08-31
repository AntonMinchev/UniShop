using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniShop.Services;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Services.Models;
using UniShop.Web.Common;
using UniShop.Web.InputModels;
using UniShop.Web.ViewModels.ParentCategories;
using X.PagedList;

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

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            ParentCategoryServiceModel parentCategoryServiceModel = new ParentCategoryServiceModel
            {
                Name = parentCategoryCreateInput.Name
            };

            this.parentCategoriesService.Create(parentCategoryServiceModel);

            return this.RedirectToAction("All");
        }

        [HttpGet("/Administration/ParentCategories/All")]
        public IActionResult All(int? pages)
        {
            var parentCategoryViewModels = this.parentCategoriesService.GetAllParentCategories().To<ParentCategoryViewModel>().ToList();

            int pageNumber = pages ?? WebConstants.DefaultPageNumber;

            var pageParentCategory = parentCategoryViewModels.ToPagedList(pageNumber, WebConstants.ParentCategoriserPageSize);


            return this.View(pageParentCategory);
        }

        [HttpGet("/Administration/ParentCategories/Edit")]
        public IActionResult Edit(int id)
        {
            var parentCategoryEditInputModel = this.parentCategoriesService.GetParentCategoryById(id).To<ParentCategoryEditInputModel>();

            if (parentCategoryEditInputModel == null)
            {
                return Redirect("All");
            }

            return this.View(parentCategoryEditInputModel);
        }

        [HttpPost("/Administration/ParentCategories/Edit")]
        public IActionResult Edit(ParentCategoryEditInputModel categoryEditInputModel)
        {
            if (!ModelState.IsValid)
            {
                var parentCategoryEditInputModel = this.parentCategoriesService.GetParentCategoryById(categoryEditInputModel.Id).To<ParentCategoryEditInputModel>();

                return this.View(parentCategoryEditInputModel);
            }


            var parentCategoryServiceModel = AutoMapper.Mapper.Map<ParentCategoryServiceModel>(categoryEditInputModel);

            this.parentCategoriesService.Edit(parentCategoryServiceModel);

            return RedirectToAction("All");
        }

        [HttpGet("/Administration/ParentCategories/Delete")]
        public IActionResult Delete(int id)
        {
            var isHaveParentCategory = this.parentCategoriesService.IsHaveParentCategory(id);

            if (!isHaveParentCategory)
            {
                this.TempData["error"] = WebConstants.ParentCategoryNonExistentMessage;
                return this.Redirect("All");
            }
            else
            {
                var isDelete = this.parentCategoriesService.Delete(id);

                if (!isDelete)
                {
                    this.TempData["error"] = WebConstants.ParentCategoryHasChildCategoriesMessage;
                    return this.Redirect("All");
                }
            }

            return this.Redirect("All");
        }
    }
}