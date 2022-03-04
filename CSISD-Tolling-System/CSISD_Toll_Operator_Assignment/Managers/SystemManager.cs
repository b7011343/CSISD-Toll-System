﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSISD_Toll_Operator_Assignment.Models;

namespace CSISD_Toll_Operator_Assignment.Data.Manager
{
    public class SystemManager
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public long createInvoice()
        {
            Invoice invoice = new Invoice() { Paid = false };
            db.Invoices.Add(invoice);
            return invoice.Id;
        }

        public long createContract()
        {
            return 0;
        }

        public bool sendInvoice()
        {
            return true;
        }

        public bool sendContract()
        {
            return true;
        }

        public bool sendInvoiceByPost()
        {
            return true;
        }
    }
}
