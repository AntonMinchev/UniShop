using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;
using UniShop.Web.InputModels;

namespace UniShop.Services.Models
{
    public class ProductServiceModel : IMapFrom<Product>, IMapTo<Product>
        ,IMapFrom<ProductCreateInputModel> ,IMapTo<ProductCreateInputModel>
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool IsInStock => this.Quantity > 0;

        public int ChildCategoryId { get; set; }

        public ChildCategoryServiceModel ChildCategory { get; set; }

        public string ChildCategoryName { get; set; }

        public string Image { get; set; }

        public virtual ICollection<ShoppingCartProductServiceModel> ShoppingCartProducts { get; set; }

        public virtual ICollection<UniShopUserFavoriteProductServiceModel> FavoriteProducts { get; set; }

        public virtual ICollection<ReviewServiceModel> Reviews { get; set; }


       
    }
}
