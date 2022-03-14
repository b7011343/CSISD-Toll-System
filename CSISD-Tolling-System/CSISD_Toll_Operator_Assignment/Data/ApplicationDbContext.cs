using System.IO;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using CSISD_Toll_Operator_Assignment.Models;

namespace CSISD_Toll_Operator_Assignment.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
    {
        public DbSet<Card>          Cards          { get; set; }
        public DbSet<Contract>      Contracts      { get; set; }
        public DbSet<Invoice>       Invoices       { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Preference>    Preferences    { get; set; }
        public DbSet<RFID>          RFIDs          { get; set; }
        public DbSet<Vehicle>       Vehicles       { get; set; }

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
