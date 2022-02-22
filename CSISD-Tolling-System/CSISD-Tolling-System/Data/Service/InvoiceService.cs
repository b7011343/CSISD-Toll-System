using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSISD_Tolling_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace CSISD_Tolling_System.Data.Service
{
    public class InvoiceService
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public InvoiceService() {}

        public List<Invoice> getUserInvoices(string userId)
        {
            return db.Invoices.Where(x => x.UserId == userId).ToList();
        }

        public List<Invoice> getUserUnpaidInvoices(string userId)
        {
            return db.Invoices.Where(x => x.UserId == userId && x.Paid == false).ToList();
        }

        public List<Invoice> getAllInvoices()
        {
            return db.Invoices.ToList();
        }

        public List<Invoice> getAllUnpaidInvoices()
        {
            return db.Invoices.Where(x => x.Paid == false).ToList();
        }
    }
}
