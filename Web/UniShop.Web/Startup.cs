using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniShop.Data;
using UniShop.Data.Models;
using CloudinaryDotNet;
using UniShop.Services;
using UniShop.Services.Mapping;
using UniShop.Web.InputModels;
using UniShop.Services.Models;
using System.Reflection;
using UniShop.Web.ViewModels;

namespace UniShop.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<UniShopDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<UniShopUser, IdentityRole>()
                .AddEntityFrameworkStores<UniShopDbContext>()
                .AddDefaultTokenProviders();


            Account cloudinaryCredentials = new Account(
              this.Configuration["Cloudinary:CloudName"],
              this.Configuration["Cloudinary:ApiKey"],
              this.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;

                options.User.RequireUniqueEmail = true;
            });

            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IParentCategoriesService, ParentCategoriesService>();
            services.AddTransient<IChildCategoriesService, ChildCategoriesService>();
            services.AddTransient<ISuppliersService, SuppliersService>();
            services.AddTransient<IProductsService, ProductsService>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            AutoMapperConfig.RegisterMappings(
              typeof(ProductCreateInputModel).GetTypeInfo().Assembly,
              typeof(ProductHomeViewModel).GetTypeInfo().Assembly,
              typeof(ProductServiceModel).GetTypeInfo().Assembly);


            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<UniShopDbContext>())
                {

                    if (!context.Roles.Any())
                    {
                        SeedRoles(context);
                    }

                }
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }



        private void SeedRoles(UniShopDbContext context)
        {
           
                context.Roles.Add(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });

                context.Roles.Add(new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                });

                context.SaveChanges();           
        }

    }
}
