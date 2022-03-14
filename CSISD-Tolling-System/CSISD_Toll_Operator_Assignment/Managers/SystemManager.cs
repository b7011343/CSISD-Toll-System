using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSISD_Toll_Operator_Assignment.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CSISD_Toll_Operator_Assignment.Data.Manager
{
    public class SystemManager
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SystemManager(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        //this method is used to create an invoice
        public long CreateInvoice()
        {
            //create new invoice with the Paid variable set to false
            Invoice invoice = new Invoice() { Paid = false };
            //add invoice to database
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                db.Invoices.Add(invoice);
                db.SaveChanges();
            }

            return invoice.Id;
        }
        //this method creates a contract
        public long CreateContract()
        {
            return 0;
        }
        //this method sends the invoice to the user - this returns true always because it outside the scope of the assignment
        public bool SendInvoice()
        {
            return true;
        }
        //this method sends the contract to the user - this returns true always because it outside the scope of the assignment
        public bool SendContract()
        {
            return true;
        }
        //this method sends the invoice by post - this returns true always because it outside the scope of the assignment
        public bool SendInvoiceByPost()
        {
            return true;
        }
    }
}
