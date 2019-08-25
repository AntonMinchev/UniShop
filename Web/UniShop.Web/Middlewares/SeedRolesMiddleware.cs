using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace UniShop.Web.Middlewares
{
    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager, "Admin").Wait();
            SeedRoles(roleManager, "User").Wait();

            await this.next(context);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager, string role)
        {
            var roleExists = await roleManager.RoleExistsAsync(role);

            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}

