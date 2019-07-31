using System.Linq;
using UniShop.Services.Models;

namespace UniShop.Services
{
    public interface IProductsService
    {
        bool Create(ProductServiceModel productServiceModel);

        IQueryable<ProductServiceModel> GetAllProducts();
    }
}
