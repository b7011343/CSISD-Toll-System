using System;
using CSISD_Tolling_System.Data;
using CSISD_Tolling_System.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CSISD_Toll_Operator_Assignment.Areas.Identity.IdentityHostingStartup))]
namespace CSISD_Toll_Operator_Assignment.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));
            });
        }
    }
}