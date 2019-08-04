using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniShop.Services;
using UniShop.Web.ViewModels;

namespace UniShop.Web.Components
{
    public class CategoryComponent : ViewComponent
    {
        private readonly IParentCategoriesService parentCategoriesService;

        public CategoryComponent(IParentCategoriesService parentCategoriesService)
        {
            this.parentCategoriesService = parentCategoriesService;
        }

        public IViewComponentResult Invoke()
        {

            var categories = this.parentCategoriesService.GetAllParentCategories().ToList();


            var categoriesViewModel = categories.Select(category => new CategoryViewModel
            {
                Name = category.Name,
                ChildCategories = category.ChildCategories
            }).ToList();

            if (categoriesViewModel.Count == 0)
            {
                return this.View(new List<ShoppingCartProductViewModel>());
            }

            return this.View();
            }

        }
}
