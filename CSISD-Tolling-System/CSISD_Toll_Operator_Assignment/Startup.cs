using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Manager;
using CSISD_Toll_Operator_Assignment.Data.Manager;
using CSISD_Toll_Operator_Assignment.Service;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

namespace CSISD_Toll_Operator_Assignment
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
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<User>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            //services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
            services.AddHttpContextAccessor();
            services.AddSingleton<SimulationManager>();
            services.AddSingleton<SystemManager>();
            services.AddTransient<PreferenceService>();
            services.AddTransient<InvoiceService>();
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();

            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            services.AddMvc()
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization();

            // Configures all the localization services
            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                            new CultureInfo("en"), // English
                            new CultureInfo("fr"), // French
                            new CultureInfo("ar"), // Arabic
                            new CultureInfo("nb"), // Norwegian
                            new CultureInfo("sv"), // Swedish
                            new CultureInfo("da"), // Danish
                            new CultureInfo("fi"), // Finnish
                    };

                    opts.DefaultRequestCulture = new RequestCulture("en");

                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;

                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;

                    // Allows language to be set by a query string parameter
                    opts.RequestCultureProviders = new List<IRequestCultureProvider>()
                    {
                        new UserPreferenceRequestCultureProvider(),
                        new QueryStringRequestCultureProvider()
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> _userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            {
                SimulationManager simulationManager = app.ApplicationServices.GetRequiredService<SimulationManager>();
                simulationManager.Generate();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "identity",
                    pattern: "Identity/{controller=Account}/{action=Login}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
