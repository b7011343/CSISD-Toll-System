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

        public long CreateInvoice()
        {
            Invoice invoice = new Invoice() { Paid = false };

            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                db.Invoices.Add(invoice);
                db.SaveChanges();
            }

            return invoice.Id;
        }

        public long CreateContract()
        {
            return 0;
        }

        public bool SendInvoice()
        {
            return true;
        }

        public bool SendContract()
        {
            return true;
        }

        public bool SendInvoiceByPost()
        {
            return true;
        }
    }
}
