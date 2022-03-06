﻿using System.Collections.Generic;
using System.Linq;
using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Data;

namespace CSISD_Toll_Operator_Assignment.Service
{
    public class InvoiceService
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public InvoiceService() { }

        public List<Invoice> GetUserInvoices(string userId)
        {
            return db.Invoices.Where(x => x.UserId == userId).ToList();
        }

        public List<Invoice> GetUserUnpaidInvoices(string userId)
        {
            return db.Invoices.Where(x => x.UserId == userId && x.Paid == false).ToList();
        }

        public List<Invoice> GetAllInvoices()
        {
            return db.Invoices.ToList();
        }

        public List<Invoice> GetAllUnpaidInvoices()
        {
            return db.Invoices.Where(x => x.Paid == false).ToList();
        }
    }
}
