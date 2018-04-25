using System;
using System.IO;
using System.Net;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console();
                
            var appConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .Build();

            string azureTableStorge = appConfiguration["ConnectionStrings:StorageAccount"];
            if (azureTableStorge != null)
            {
                string tableName = appConfiguration["ConnectionStrings:StorageTableLogging"];
                loggerConfiguration.WriteTo.AzureTableStorage(azureTableStorge, storageTableName: tableName);
            }

            string logToFile = appConfiguration["LogToFile"];
            if (logToFile != null )
            {
                loggerConfiguration.WriteTo.File(logToFile,
                    fileSizeLimitBytes: 1_000_000,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1));
            }

            Log.Logger = loggerConfiguration.CreateLogger();
            
            try
            {
                Log.Information("Starting web host");
                var host = BuildWebHost(args);

                //DropAndRecreateDatabase(host);
                MigrateAndSeedDatabase(host);

                host.Run();
                
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
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
                .UseKestrel(options =>  
                { 
                    options.Listen(IPAddress.Loopback, 5000, listenOptions => 
                    { 
                        listenOptions.UseHttps("development.arkitektum.pfx", "password"); 
                    }); 
                }) 
                .UseStartup<Startup>()
                .UseSerilog()
                .UseUrls("https://localhost:5000")
                .Build();
        }
    }
}