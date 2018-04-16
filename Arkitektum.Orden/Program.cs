using System;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Serilog;
using Serilog.Events;

namespace Arkitektum.Orden
{
    public class Program
    {
        public static int Main(string[] args)
        {
                var host = BuildWebHost(args);

                //DropAndRecreateDatabase(host);
                MigrateAndSeedDatabase(host);

                host.Run();

                return 0;
         
        }
        
        private static void DropAndRecreateDatabase(IWebHost host)
        {
            Log.Information("Dropping and recreating the database");
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    DbInitializer.Initialize(context, userManager, roleManager).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while recreating the database");
                }
            }
        }

        private static void MigrateAndSeedDatabase(IWebHost host)
        {
            Log.Information("Running migrations and seeding data");
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    context.Database.Migrate();
                    DbInitializer.Initialize(context, userManager, roleManager).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }
    }
}