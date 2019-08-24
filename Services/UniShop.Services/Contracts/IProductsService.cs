using System.Collections.Generic;
using System.Linq;
using UniShop.Data.Models.Enums;
using UniShop.Services.Models;

namespace UniShop.Services.Contracts
{
    public interface IProductsService
    {
        bool Create(ProductServiceModel productServiceModel);

        IQueryable<ProductServiceModel> GetAllProducts();

        IQueryable<ProductServiceModel> GetAllProducts(int? childCategoryId,string searchString);

        IQueryable<ProductServiceModel> SortProducts(IQueryable<ProductServiceModel> products,ProductsSort sort);

        ProductServiceModel GetById(int id);

        bool ReduceProductQuantity(int productId,int quantity);

        bool Edit(ProductServiceModel productServiceModel);
    }
}
