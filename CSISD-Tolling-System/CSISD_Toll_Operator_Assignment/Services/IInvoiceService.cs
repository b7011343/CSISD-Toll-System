﻿using CSISD_Toll_Operator_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Toll_Operator_Assignment.Services
{
    public interface IInvoiceService
    {
        //this method gets all invoices linked to the userId specified
        List<Invoice> GetUserInvoices(string userId);
        //this method gets all unpaid invoices linked to the userId specified
        List<Invoice> GetUserUnpaidInvoices(string userId);
        //this method gets all invoices
        List<Invoice> GetAllInvoices();
        //this method gets all unpaid invoices
        List<Invoice> GetAllUnpaidInvoices();
    }
}
