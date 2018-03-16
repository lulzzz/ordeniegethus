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
            
            
            var emailRegularUser = "henning@arkitektum.no";
            var regularUser = new ApplicationUser
            {
                UserName = emailRegularUser,
                Email = emailRegularUser,
                FullName = "Administrator"
            };
            IdentityResult resultRegularUser = userManager.CreateAsync(regularUser, "Test123").Result;
            if (result.Succeeded) await userManager.AddToRoleAsync(regularUser, Roles.User);

            

            //adding Kommuner
            var boKommune = new Organization() {Name = "Bø kommune"};
            context.Add(boKommune);
            var sauheradKommune = new Organization() {Name = "Sauherad kommune"};
            context.Add(sauheradKommune);
            var skienKommune = new Organization() {Name = "Skien kommune"};
            context.Add(skienKommune);
            var kongsbergKommune = new Organization() {Name = "Kongsberg kommune"};
            context.Add(kongsbergKommune);

            
            regularUser.Organizations = new List<OrganizationApplicationUser>()
            {
                new OrganizationApplicationUser()
                {
                    ApplicationUser = regularUser,
                    Organization = boKommune,
                    Role = Roles.OrganizationAdmin
                },
                new OrganizationApplicationUser()
                {
                    ApplicationUser = regularUser,
                    Organization = skienKommune,
                    Role = Roles.OrganizationAdmin
                },
            };
            

            // bø kommune
            var sectorBoBarnehage = new Sector() { Name = "Barnehage", Organization = boKommune };
            context.Add(sectorBoBarnehage);

            var sectorBoKultur = new Sector() { Name = "Kultur", Organization = boKommune };
            context.Add(sectorBoKultur);

            var sectorBoGrunnskole = new Sector() { Name = "Grunnskole", Organization = boKommune };
            context.Add(sectorBoGrunnskole);

            var sectorBoSosialtjenesten = new Sector() { Name = "Sosialtjenesten", Organization = boKommune };
            context.Add(sectorBoSosialtjenesten);

            var sectorBoBarnevern = new Sector() { Name = "Barnevern", Organization = boKommune };
            context.Add(sectorBoBarnevern);
            
            context.Add(AddApplication("itsLearning", boKommune, sectorBoGrunnskole));
            context.Add(AddApplication("Docebo LMS", boKommune, sectorBoGrunnskole));
            context.Add(AddApplication("LearnUpon", boKommune, sectorBoGrunnskole));
            context.Add(AddApplication("Schoology LMS", boKommune, sectorBoGrunnskole));
            context.Add(AddApplication("Google Classroom", boKommune, sectorBoGrunnskole));

            context.Add(AddApplication("vismaBarnehage", boKommune, sectorBoBarnehage));
            context.Add(AddApplication("IST barnehage", boKommune, sectorBoBarnehage));
            context.Add(AddApplication("Fagsystem Oppvekst", boKommune, sectorBoBarnehage));

            context.Add(AddApplication("Fagsystem Kultur", boKommune, sectorBoKultur));
            context.Add(AddApplication("IST Extens", boKommune, sectorBoKultur));

            context.Add(AddApplication("Fagsystem Sosial", boKommune, sectorBoSosialtjenesten));


            // skien

            var sectorSkienKultur = new Sector() { Name = "Kultur", Organization = skienKommune };
            context.Add(sectorBoKultur);

            var sectorSkienGrunnskole = new Sector() { Name = "Grunnskole", Organization = skienKommune };
            context.Add(sectorBoGrunnskole);

            var sectorSkienSosialtjenesten = new Sector() { Name = "Sosialtjenesten", Organization = skienKommune };
            context.Add(sectorBoSosialtjenesten);

            context.Add(AddApplication("itsLearning", skienKommune, sectorSkienGrunnskole));
            context.Add(AddApplication("Schoology LMS", skienKommune, sectorSkienGrunnskole));
            
            context.Add(AddApplication("Fagsystem Kultur", skienKommune, sectorSkienKultur));

            context.Add(AddApplication("Fagsystem Sosial", skienKommune, sectorSkienSosialtjenesten));

            context.SaveChanges();
        }
    }
}