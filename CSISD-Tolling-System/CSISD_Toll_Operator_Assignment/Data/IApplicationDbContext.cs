using CSISD_Toll_Operator_Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Toll_Operator_Assignment.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Card> Cards                   { get; set; }
        public DbSet<Contract> Contracts           { get; set; }
        public DbSet<Invoice> Invoices             { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Preference> Preferences       { get; set; }
        public DbSet<RFID> RFIDs                   { get; set; }
        public DbSet<Vehicle> Vehicles             { get; set; }
    }
}
