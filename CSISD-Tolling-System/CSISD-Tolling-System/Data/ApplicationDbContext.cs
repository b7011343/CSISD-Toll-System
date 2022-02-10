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
        public DbSet<Card> Cards { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<RFID> RFIDs { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public ApplicationDbContext() {}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("AspNetUsers");
            modelBuilder.Entity<Card>().ToTable("Card");
            modelBuilder.Entity<Contract>().ToTable("Contract");
            modelBuilder.Entity<Invoice>().ToTable("Invoice");
            modelBuilder.Entity<PaymentMethod>().ToTable("PaymentMethod");
            modelBuilder.Entity<Preference>().ToTable("Preference");
            modelBuilder.Entity<RFID>().ToTable("RFID");
            modelBuilder.Entity<Vehicle>().ToTable("Vehicle");
        }
    }
}
