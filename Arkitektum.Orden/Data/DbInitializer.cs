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


            //adding Kommuner
            var boKommune = new Organization() {Name = "Bø kommune"};
            context.Add(boKommune);
            var sauheradKommune = new Organization() {Name = "Sauherad kommune"};
            context.Add(sauheradKommune);
            var skienKommune = new Organization() {Name = "Skien kommune"};
            context.Add(skienKommune);
            var kongsbergKommune = new Organization() {Name = "Kongsberg kommune"};
            context.Add(kongsbergKommune);

            //adding Tjenesteområder
            var sectorBarnehage = new Sector() { Name = "Barnehage", Organization = boKommune };
            context.Add(sectorBarnehage);

            var sectorKultur = new Sector() { Name = "Kultur", Organization = boKommune };
            context.Add(sectorKultur);

            var sectorGrunnskole = new Sector() { Name = "Grunnskole", Organization = boKommune };
            context.Add(sectorGrunnskole);

            var sectorSosialtjenesten = new Sector() { Name = "Sosialtjenesten", Organization = boKommune };
            context.Add(sectorSosialtjenesten);

            var sectorBarnevern = new Sector() { Name = "Barnevern", Organization = boKommune };
            context.Add(sectorBarnevern);



            /*
        var vismaBarnehage = new Application() { Name = "Visma barnehage", Organization = boKommune};
        vismaBarnehage.SectorApplications = new List<SectorApplication>()
        {
            new SectorApplication()
            {
                Application = vismaBarnehage,
                Sector = sectorBarnehage
            }

        };
        */


            //adding Applikasjoner
            context.Add(AddApplication("itsLearning", boKommune, sectorGrunnskole));
            context.Add(AddApplication("Docebo LMS", boKommune, sectorGrunnskole));
            context.Add(AddApplication("LearnUpon", boKommune, sectorGrunnskole));
            context.Add(AddApplication("Schoology LMS", boKommune, sectorGrunnskole));
            context.Add(AddApplication("Google Classroom", boKommune, sectorGrunnskole));


            context.Add(AddApplication("vismaBarnehage", boKommune, sectorBarnehage));
            context.Add(AddApplication("IST barnehage", boKommune, sectorBarnehage));
            context.Add(AddApplication("Fagsystem Oppvekst", boKommune, sectorBarnehage));

            context.Add(AddApplication("Fagsystem Kultur", boKommune, sectorKultur));
            context.Add(AddApplication("IST Extens", boKommune, sectorKultur));

            context.Add(AddApplication("Fagsystem Sosial", boKommune, sectorSosialtjenesten));

            context.SaveChanges();
        }
    }
}