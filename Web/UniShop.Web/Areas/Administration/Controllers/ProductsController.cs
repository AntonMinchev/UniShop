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
using UniShop.Web.ViewModels;
using UniShop.Web.ViewModels.Products;
using X.PagedList;

namespace UniShop.Web.Areas.Administration.Controllers
{
    public class ProductsController : AdminController
    {
        private readonly IChildCategoriesService childCategoriesService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IProductsService productsService;

        public ProductsController(IChildCategoriesService childCategoriesService,ICloudinaryService cloudinaryService,IProductsService productsService)
        {
            this.childCategoriesService = childCategoriesService;
            this.cloudinaryService = cloudinaryService;
            this.productsService = productsService;
        }

        [HttpGet("/Administration/Products/Create")]
        public IActionResult Create()
        {
            var childCategories = this.childCategoriesService.GetAllChildCategories();

            this.ViewData["categories"] = childCategories.Select(childCategory => new ProductCreateChildCategoryViewModel
            {
                ParentCategoryName = childCategory.ParentCategory.Name,
                Name = childCategory.Name
            })
                .ToList(); ;

            return this.View();
        }


        [HttpPost("/Administration/Products/Create")]
        public IActionResult Create(ProductCreateInputModel productCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            string pictureUrl = this.cloudinaryService.UploadPicture(
                productCreateInputModel.Image,
                productCreateInputModel.Name);

            ProductServiceModel productServiceModel = AutoMapper.Mapper.Map<ProductServiceModel>(productCreateInputModel);

            productServiceModel.Image = pictureUrl;

            this.productsService.Create(productServiceModel);

            return this.Redirect("/");            

        }

        [HttpGet("/Administration/Products/All")]
        public IActionResult All(int? pages)
        {
            var productsViewModels = this.productsService.GetAllProducts().To<ProductAllViewModel>().ToList();

            int pageNumber = pages ?? WebConstants.DefaultPageNumber;

            var pageProductsViewModels = productsViewModels.ToPagedList(pageNumber, WebConstants.ProductsPageSize);

            return this.View(pageProductsViewModels);

        }

        [HttpGet("/Administration/Products/Edit")]
        public IActionResult Edit(int id)
        {
            var productEditInputModel = this.productsService.GetById(id).To<ProductEditInputModel>();

            if (productEditInputModel == null)
            {
                return Redirect("All");
            }

            var childCategories = this.childCategoriesService.GetAllChildCategories();


            this.ViewData["categories"] = childCategories.Select(childCategory => new ProductCreateChildCategoryViewModel
            {
                ParentCategoryName = childCategory.ParentCategory.Name,
                Name = childCategory.Name
            })
              .ToList(); ;

            return this.View(productEditInputModel);

        }

        [HttpPost("/Administration/Products/Edit")]
        public IActionResult Edit(ProductEditInputModel productEditInputModel)
        {
            if (!ModelState.IsValid)
            {
                var productEditModel = this.productsService.GetById(productEditInputModel.Id).To<ProductEditInputModel>();

                var childCategories = this.childCategoriesService.GetAllChildCategories();

                this.ViewData["categories"] = childCategories.Select(childCategory => new ProductCreateChildCategoryViewModel
                {
                    ParentCategoryName = childCategory.ParentCategory.Name,
                    Name = childCategory.Name
                })
                  .ToList(); ;

                return this.View(productEditModel);
            }

            string pictureUrl = this.cloudinaryService.UploadPicture(
                productEditInputModel.Image,
                productEditInputModel.Name);

            var productServiceModel = AutoMapper.Mapper.Map<ProductServiceModel>(productEditInputModel);

            productServiceModel.Image = pictureUrl;

            this.productsService.Edit(productServiceModel);

            return this.Redirect("All");
        }

    }
}
