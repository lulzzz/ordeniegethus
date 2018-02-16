using System.Linq;
using Arkitektum.Orden.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Arkitektum.Orden.Data
{
    public static class ApplicationDbContextExtensions
    {
        public static async void EnsureSeedData(this ApplicationDbContext context, IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();


                if (!context.Database.GetPendingMigrations().Any())
                {
                    foreach (var role in Roles.All)
                        if (!roleManager.RoleExistsAsync(role.ToUpper()).Result)
                            await roleManager.CreateAsync(new IdentityRole {Name = role});

                    if (!context.Users.Any())
                    {
                        var email = "dev@arkitektum.no";
                        var adminUser = new ApplicationUser
                        {
                            UserName = email,
                            Email = email
                        };
                        var result = userManager.CreateAsync(adminUser, "Test123").Result;

                        if (result.Succeeded)
                        {
                            await userManager.AddToRolesAsync(adminUser, Roles.All);
                        }
                            
                    }
                }
            }
        }
    }
}