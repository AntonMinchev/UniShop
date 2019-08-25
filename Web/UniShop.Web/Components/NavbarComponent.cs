using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniShop.Services;
using UniShop.Services.Contracts;
using UniShop.Services.Mapping;
using UniShop.Web.ViewModels;
using UniShop.Web.ViewModels.ChildCategories;
using UniShop.Web.ViewModels.ShoppingCarts;

namespace UniShop.Web.Components
{
    public class NavbarComponent : ViewComponent
    {
        private readonly IParentCategoriesService parentCategoriesService;

        public NavbarComponent(IParentCategoriesService parentCategoriesService)
        {
            this.parentCategoriesService = parentCategoriesService;
        }

        public IViewComponentResult Invoke()
        {

            var categories = this.parentCategoriesService.GetAllParentCategories().To<CategoryViewModel>().ToList();
            
            //var categoriesViewModel = categories.Select(category => new CategoryViewModel
            //{
            //    Name = category.Name,
            //    ChildCategories = category.ChildCategories
            //}).ToList();

            if (categories.Count() == 0)
            {
                return this.View(new List<CategoryViewModel>());
            }

            return this.View(categories);
        }

    }
}
