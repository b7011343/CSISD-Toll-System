using CSISD_Toll_Operator_Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace CSISD_Toll_Operator_Assignment.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Card> Cards                   { get; set; }
        DbSet<Contract> Contracts           { get; set; }
        DbSet<Invoice> Invoices             { get; set; }
        DbSet<PaymentMethod> PaymentMethods { get; set; }
        DbSet<Preference> Preferences       { get; set; }
        DbSet<RFID> RFIDs                   { get; set; }
        DbSet<Vehicle> Vehicles             { get; set; }
    }
}
