using System.Security.Principal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Utils;
using Arkitektum.Orden.Services.Search;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;

namespace Arkitektum.Orden
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
            services.AddAntiforgery(opts => {
                opts.Cookie.Name = "AntiForgery.OrdenIEgetHus";
                opts.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });
            services.AddDataProtection().SetApplicationName("OrdenIEgetHus");

            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true; // cookie is only available on server side
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .EnableSensitiveDataLogging()
                );

            services.AddIdentity<ApplicationUser, IdentityRole>(
                    user =>
                    {
                        user.Password.RequireNonAlphanumeric = false;
                    })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // get configuration from appsettings.json - use as singleton
            AppSettings appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            services.AddSingleton(appSettings);

            // Add application services.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<INationalComponentService, NationalComponentService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<ISearchIndexingService, ElasticSearchIndexingService>();
            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<ISecurityHelper, SecurityHelper>();
            services.AddTransient<ISectorService, SectorService>();
            services.AddTransient<IDatasetService, DatasetService>();
            services.AddTransient<ICookieHelper, CookieHelper>();
            services.AddTransient<IResourceLinkService, ResourceLinkService>();
            services.AddTransient<ISearchService, ElasticSearchService>();
            services.AddTransient<ISuperUsersService, SuperUsersService>();

            services.AddMvc()
                .AddMvcOptions(m => m.ModelMetadataDetailsProviders.Add(new LocalizedDisplayMetadataProvider()));

            services.AddMvc()
            .AddDataAnnotationsLocalization(options => {
                 options.DataAnnotationLocalizerProvider = (type, factory) =>
                     factory.Create(typeof(UIResource));
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                TelemetryConfiguration.Active.DisableTelemetry = true;

                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSession();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}

                
