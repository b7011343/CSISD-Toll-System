using System;
using CSISD_Tolling_System.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CSISD_Tolling_System.Areas.Identity.IdentityHostingStartup))]
namespace CSISD_Tolling_System.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CSISD_Tolling_SystemContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CSISD_Tolling_SystemContextConnection")));

                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<CSISD_Tolling_SystemContext>();
            });
        }
    }
}