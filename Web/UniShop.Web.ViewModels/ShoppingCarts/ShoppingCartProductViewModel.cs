using UniShop.Services.Models;

namespace UniShop.Web.ViewModels.ShoppingCarts
{
    public class ShoppingCartProductViewModel 
    {
        public int Id { get; set; }

        public ProductServiceModel Product { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
       
    }
}
