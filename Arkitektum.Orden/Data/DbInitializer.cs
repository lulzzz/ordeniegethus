using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Microsoft.AspNetCore.Identity;

namespace Arkitektum.Orden.Data
{

       public static class DbInitializer
    {


        public static Application AddApplication(string name, Organization organization, Sector sector)
        {
            var application = new Application() { Name = name, Organization = organization };
            application.SectorApplications = new List<SectorApplication>()
                {
                    new SectorApplication()
                    {
                        Application = application,
                        Sector = sector
                    }
                };
            return application;
        }
        

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

            // add organizations
            context.Add(new Organization() { Name = "Bø kommune", OrganizationNumber = "962276172" });
            context.Add(new Organization() { Name = "Sauherad kommune", OrganizationNumber = "964963460" });
            context.Add(new Organization() { Name = "Skien kommune", OrganizationNumber = "938759839" });
            context.Add(new Organization() { Name = "Kongsberg kommune", OrganizationNumber = "942402465" });
           
            // add sectors
            context.Add(new Sector { Name = "Plan, bygg og geodata" });
            context.Add(new Sector { Name = "Helse, sosial og omsorg" });
            context.Add(new Sector { Name = "Oppvekst og utdanning" });
            context.Add(new Sector { Name = "Kultur, idrett og fritid" });
            context.Add(new Sector { Name = "Trafikk, reiser og samferdsel" });
            context.Add(new Sector { Name = "Natur og miljø" });
            context.Add(new Sector { Name = "Næringsutvikling" });
            context.Add(new Sector { Name = "Skatter og avgifter" });
            context.Add(new Sector { Name = "Tekniske tjenester" });
            context.Add(new Sector { Name = "Administrasjon" });

            // add national components
            context.Add(new NationalComponent { Name = "Altinn"});
            context.Add(new NationalComponent { Name = "Det sentrale folkeregisteret"});
            context.Add(new NationalComponent { Name = "Digital postkasse til innbyggere"});
            context.Add(new NationalComponent { Name = "Enhetsregisteret"});
            context.Add(new NationalComponent { Name = "ID-porten"});
            context.Add(new NationalComponent { Name = "Kontakt- og reservasjonsregisteret"});
            context.Add(new NationalComponent { Name = "Matrikkelen"});

            context.SaveChanges();
        }
    }
}