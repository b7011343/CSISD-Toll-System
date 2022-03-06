using System.Collections.Generic;
using System.Linq;
using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Services;

namespace CSISD_Toll_Operator_Assignment.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IApplicationDbContext _db;

        public InvoiceService(IApplicationDbContext db)
        {
            _db = db;
        }

        public List<Invoice> GetUserInvoices(string userId)
        {
            return _db.Invoices.Where(x => x.UserId == userId).ToList();
        }

        public List<Invoice> GetUserUnpaidInvoices(string userId)
        {
            return _db.Invoices.Where(x => x.UserId == userId && x.Paid == false).ToList();
        }

        public List<Invoice> GetAllInvoices()
        {
            return _db.Invoices.ToList();
        }

        public List<Invoice> GetAllUnpaidInvoices()
        {
            return _db.Invoices.Where(x => x.Paid == false).ToList();
        }
    }
}
