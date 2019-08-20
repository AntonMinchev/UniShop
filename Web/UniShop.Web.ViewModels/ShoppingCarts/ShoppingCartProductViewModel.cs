using UniShop.Services.Models;
using UniShop.Web.ViewModels.Products;

namespace UniShop.Web.ViewModels.ShoppingCarts
{
    public class ShoppingCartProductViewModel 
    {
        public int Id { get; set; }

        public ProductViewModel Product { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
       
    }
}
