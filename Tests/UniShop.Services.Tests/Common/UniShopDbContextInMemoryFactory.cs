using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniShop.Data;

namespace UniShop.Services.Tests.Common
{
    public static class UniShopDbContextInMemoryFactory
    {
        public static UniShopDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<UniShopDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            return new UniShopDbContext(options);
        }
    }
}
