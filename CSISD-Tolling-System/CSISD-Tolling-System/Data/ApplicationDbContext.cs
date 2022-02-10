using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CSISD_Tolling_System.Models;

namespace CSISD_Tolling_System.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Card> Cards { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<RFID> RFIDs { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
