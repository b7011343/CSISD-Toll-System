using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Service;
using CSISD_Toll_Operator_Assignment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Toll_Operator_Assignment.Models
{
    public class IndexViewModel
    {
        public List<Invoice> Invoices { get; set; }
        public string UserId { get; set; }

        public IndexViewModel(string role, User user, IInvoiceService invoiceService)
        {
            UserId = user.Id;

            switch (role)
            {
                case Roles.RoadUser:
                    Invoices = invoiceService.GetUserUnpaidInvoices(UserId);
                    break;

                case Roles.TollOperator:
                    Invoices = invoiceService.GetAllUnpaidInvoices();
                    break;

                default:
                    break;
            }
        }
    }
}
