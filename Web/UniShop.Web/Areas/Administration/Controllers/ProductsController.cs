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

            this.ViewData["types"] = childCategories.Select(childCategory => new ProductCreateChildCategoryViewModel
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
            string pictureUrl = this.cloudinaryService.UploadPicture(
                productCreateInputModel.Image,
                productCreateInputModel.Name);

            ProductServiceModel productServiceModel = AutoMapper.Mapper.Map<ProductServiceModel>(productCreateInputModel);

            productServiceModel.Image = pictureUrl;

            this.productsService.Create(productServiceModel);

            return this.Redirect("/");            

        }

        [HttpGet("/Administration/Products/All")]
        public IActionResult All(ProductCreateInputModel productCreateInputModel)
        {
            var productsViewModels = this.productsService.GetAllProducts().To<ProductViewModel>().ToList();

            return this.View(productsViewModels);

        }
    }
}