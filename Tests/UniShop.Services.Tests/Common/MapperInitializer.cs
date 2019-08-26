using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UniShop.Data.Models;
using UniShop.Services.Mapping;
using UniShop.Services.Models;

namespace UniShop.Services.Tests.Common
{
    public static class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(ProductServiceModel).GetTypeInfo().Assembly,
                typeof(Product).GetTypeInfo().Assembly);
        }
    }
}
