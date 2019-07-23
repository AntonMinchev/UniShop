using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniShop.Data.Models;

namespace UniShop.Data
{
    public class UniShopDbContext : IdentityDbContext<UniShopUser,IdentityRole,string>
    {
        public UniShopDbContext(DbContextOptions options) : base(options)
        {
        }

      
    }
}

