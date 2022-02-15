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
            modelBuilder.Entity<Card>().ToTable("Card").Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Contract>().ToTable("Contract").Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Invoice>().ToTable("Invoice").Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<PaymentMethod>().ToTable("PaymentMethod").Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Preference>().ToTable("Preference").Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<RFID>().ToTable("RFID").Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Vehicle>().ToTable("Vehicle").Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
