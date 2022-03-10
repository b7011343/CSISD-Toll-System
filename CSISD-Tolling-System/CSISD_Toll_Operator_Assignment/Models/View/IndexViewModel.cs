using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Services;
using System.Collections.Generic;

namespace CSISD_Toll_Operator_Assignment.Models
{
    /// <summary>
    /// Model for the default (non administrator) index pages.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// List of invoices visible to the current user.
        /// (either all unpaid invoices for a toll operator or just the users
        /// unpaid invoices for a road user)
        /// </summary>
        public List<Invoice> Invoices { get; set; }

        /// <summary>
        /// ID of the logged in user, only applicable for road users.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Create a new IndexViewModel and populate the Invoices list depending
        /// on role.
        /// </summary>
        /// <param name="role">The role of the user</param>
        /// <param name="user">The user object iself.</param>
        /// <param name="invoiceService">Service to get invoice information</param>
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
