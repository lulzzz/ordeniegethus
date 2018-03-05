using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Microsoft.AspNetCore.Identity;

namespace Arkitektum.Orden.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            
            if (context.ApplicationUser.Any()) 
                return; // DB has been seeded


            foreach (var role in Roles.All)
                if (!roleManager.RoleExistsAsync(role.ToUpper()).Result)
                    await roleManager.CreateAsync(new IdentityRole {Name = role});

            
            var email = "dev@arkitektum.no";
            var adminUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = "Administrator"
            };
            IdentityResult result = userManager.CreateAsync(adminUser, "Test123").Result;

            if (result.Succeeded) await userManager.AddToRolesAsync(adminUser, Roles.All);

            var boKommune = new Organization() {Name = "Bø kommune"};
            context.Add(boKommune);
            var sauheradKommune = new Organization() {Name = "Sauherad kommune"};
            context.Add(sauheradKommune);
            var skienKommune = new Organization() {Name = "Skien kommune"};
            context.Add(skienKommune);
            var kongsbergKommune = new Organization() {Name = "Kongsberg kommune"};
            context.Add(kongsbergKommune);

            context.SaveChanges();
        }
    }
}