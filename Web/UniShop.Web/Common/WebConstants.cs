using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniShop.Web.Common
{
    public static class WebConstants
    {
        public const string AdminRoleName = "Admin";

        public const string UserRoleName = "User";

        public const int DefaultIndexPageSize = 9;

        public const int DefaultPageNumber = 1;

        public const int FavoriteProductsPageSize = 10;

        public const int OrdersPageSize = 10;

        public const int ReviewsPageSize = 5;

        public const string ErrorOrderMessage = "Проверете дали са налични продуктите , които искате да поръчате и опитайте отното!";

        public const string IncreaseShoppingCartMessage = "Не може да поръчате от наличното количество за даден продукт!!! ";

        public const string ReduceShoppingCartMessage = "Не може да поръчате по-малко от 1 продукт,ако искате да не го поръчвате го премахнете от количката!!! ";

    }
}
