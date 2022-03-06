using CSISD_Toll_Operator_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Toll_Operator_Assignment.Services
{
    public interface IInvoiceService
    {
        List<Invoice> GetUserInvoices(string userId);
        List<Invoice> GetUserUnpaidInvoices(string userId);
        List<Invoice> GetAllInvoices();
        List<Invoice> GetAllUnpaidInvoices();
    }
}
