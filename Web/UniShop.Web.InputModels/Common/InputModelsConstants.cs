using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Web.InputModels.Common
{
    public static class InputModelsConstants
    {
        public const string RequiredErrorMessage = "Полето \"{0}\" е задължително.";

        public const string StringLengthErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1} символа.";

        public const string PriceErrorMessage = "Цената трябва да е между {1} и {2} лв!";

        public const string QuantityErrorMessage = "Количеството на продуктите  трябва да е число от {1} до {2}!";

        public const string City = "Град";

        public const string Street = "Адрес";

        public const string BuildingNumber = "Номер на сградата";

        public const string Name = "Име";

        public const string Price = "Цена";

        public const string Description = "Описание";

        public const string Specification = "Характеристика";

        public const string Quantity = "Количество";

        public const string Image = "Снимка";

        public const string ChildCategoryName = "Категория";

        public const string Raiting = "Рейтинг";

        public const string Comment = "Коментар";

        public const string PriceToOffice = "Цена до офис";

        public const string PriceToHome = "Цена до адрес";

        public const int SupplierMinLength = 3;

        public const int SupplierMaxLength = 20;

        public const int RaitingMinNumber = 0;

        public const int RaitingMaxNumber = 5;

        public const int CommentMinLength = 5;

        public const int CommentMaxLength = 150;

        public const int ProductMinLength = 3;

        public const int ProductMaxLength = 30;

        public const string PriceMinValue = "0.01";

        public const string PriceMaxValue = "79228162514264337593543950335";

        public const int QuantityMinNumber = 1;

        public const int QuantityMaxNumber = int.MaxValue;

        public const int DescriptionMinLength = 10;

        public const int DescriptionMaxLength = 256;

        public const int SpecificationMinLength = 10;

        public const int SpecificationMaxLength = 256;        

        public const int CityMinLength = 3;

        public const int CityMaxLength = 30;

        public const int StreetMinLength = 3;

        public const int StreetMaxLength = 50;

        public const int BuildingNumberMinNumber = 1;

        public const int BuildingNumberMaxNumber = 1000;

        public const int ChildCategoryMinLength = 3;

        public const int ChildCategoryMaxLength = 20;

        public const int ParentCategoryMinLength = 3;

        public const int ParentCategoryMaxLength = 20; 
    }
}
