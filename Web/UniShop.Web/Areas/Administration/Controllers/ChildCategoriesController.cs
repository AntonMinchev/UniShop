﻿using System;
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
using UniShop.Web.ViewModels;
using UniShop.Web.ViewModels.ChildCategories;
using X.PagedList;

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

            this.ViewData["categories"] = parentCategories.Select(parentCategory => new ChildCategoryCreateParentCategoryViewModel
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
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            ChildCategoryServiceModel childCategoryServiceModel = childCategoryCreateInputModel.To<ChildCategoryServiceModel>();

            this.childCategoriesService.Create(childCategoryServiceModel);

            return RedirectToAction("All");
        }

        [HttpGet("/Administration/ChildCategories/All")]
        public IActionResult All(int? pages)
        {
            var childCategoryViewModels = this.childCategoriesService.GetAllChildCategories().To<ChildCategoryViewModel>().ToList();

            int pageNumber = pages ?? WebConstants.DefaultPageNumber;

            var pageChildCategory = childCategoryViewModels.ToPagedList(pageNumber, WebConstants.ChildCategoriesPageSize);



            return this.View(pageChildCategory);
        }

        [HttpGet("/Administration/ChildCategories/Edit")]
        public IActionResult Edit(int id)
        {
            var childCategory = this.childCategoriesService.GetChildCategoryById(id).To<ChildCategoryEditInputModel>();

            if (childCategory == null)
            {
                return Redirect("All");
            }

            var parentCategories = this.parentCategoriesService.GetAllParentCategories();

            this.ViewData["categories"] = parentCategories.Select(parentCategory => new ChildCategoryCreateParentCategoryViewModel
            {
                Id = parentCategory.Id,
                Name = parentCategory.Name
            })
            .ToList(); 


            return this.View(childCategory);
        }

        [HttpPost("/Administration/ChildCategories/Edit")]
        public IActionResult Edit(ChildCategoryEditInputModel childCategoryEditInput)
        {
            if (!ModelState.IsValid)
            {
                var childCategory = this.childCategoriesService.GetChildCategoryById(childCategoryEditInput.Id).To<ChildCategoryEditInputModel>();

                var parentCategories = this.parentCategoriesService.GetAllParentCategories();

                this.ViewData["categories"] = parentCategories.Select(parentCategory => new ChildCategoryCreateParentCategoryViewModel
                {
                    Id = parentCategory.Id,
                    Name = parentCategory.Name
                })
                .ToList();


                return this.View(childCategory);
            }

            var childCategoryServiceModel = AutoMapper.Mapper.Map<ChildCategoryServiceModel>(childCategoryEditInput);

            this.childCategoriesService.Edit(childCategoryServiceModel);


            return RedirectToAction("All");
        }


        [HttpGet("/Administration/ChildCategories/Delete")]
        public IActionResult Delete(int id)
        {
            var isHaveChildCategory = this.childCategoriesService.IsHaveChildCategoryWhitId(id);

            if (!isHaveChildCategory)
            {
                this.TempData["error"] = WebConstants.ChildCategoryNonExistentMessage;
                return this.Redirect("All");
            }
            else
            {
                var isDelete = this.childCategoriesService.Delete(id);

                if (!isDelete)
                {
                    this.TempData["error"] = WebConstants.ChildCategoryWhitProductsMessage;
                    return this.Redirect("All");
                }
            }

            return this.Redirect("All");
        }
    }
}