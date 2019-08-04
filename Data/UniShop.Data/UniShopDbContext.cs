using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniShop.Data.Models;

namespace UniShop.Data
{
    public class UniShopDbContext : IdentityDbContext<UniShopUser,IdentityRole,string>
    {
        public DbSet<City> Cities { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ParentCategory> ParentCategories { get; set; }

        public DbSet<ChildCategory> ChildCategories { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

      //  public DbSet<CategoryProduct> CategoryProducts { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }

        public DbSet<UniShopUserFavoriteProduct> UniShopFavoriteProducts { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
        public UniShopDbContext(DbContextOptions<UniShopDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderProduct>().HasKey(x => new { x.OrderId, x.ProductId });

            builder.Entity<ShoppingCartProduct>().HasKey(x => new { x.ProductId, x.ShoppingCartId });

       //     builder.Entity<CategoryProduct>().HasKey(x => new { x.ChildCategoryId, x.ProductId });

            builder.Entity<UniShopUserFavoriteProduct>().HasKey(x => new { x.ProductId, x.UniShopUserId });

            builder.Entity<Product>()
                   .HasOne(x => x.ChildCategory)
                   .WithMany(x => x.Products)
                   .HasForeignKey(x => x.ChildCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ShoppingCart>()
                   .HasOne(x => x.User)
                   .WithOne(x => x.ShoppingCart)
                   .HasForeignKey<UniShopUser>(x => x.ShoppingCartId)
                   .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }

    }
}

