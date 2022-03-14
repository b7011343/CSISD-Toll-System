using System.Collections.Generic;

using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Services;

namespace CSISD_Toll_Operator_Assignment.Models
{
    public class TollHistoryViewModel
    {
        /// <summary>
        /// List of invoices visible on the page
        /// </summary>
        public List<Invoice> Invoices { get; set; }
<<<<<<< HEAD
        /// <summary>
        /// The userId linked to all of the invoices
        /// </summary>
        public string UserId { get; set; }
=======
        public string        UserId   { get; set; }
>>>>>>> 7297ed2f7b6b233950b559c4a1eb682bea329f17

        public TollHistoryViewModel(string role, User user, IInvoiceService invoiceService)
        {
            UserId = user.Id;

            switch (role)
            {
                case Roles.RoadUser:
                    Invoices = invoiceService.GetUserInvoices(UserId);
                    break;

                case Roles.TollOperator:
                    Invoices = invoiceService.GetAllInvoices();
                    break;

                default:
                    break;
            }
        }
    }
}
