using System.Collections.Generic;

namespace CSISD_Toll_Operator_Assignment.Models
{
    public class PaymentViewModel
    {
        /// <summary>
        /// The Invoice visible on the page
        /// </summary>
        public Invoice invoice { get; set; }
        /// <summary>
        /// List of cards visible to the logged in user
        /// </summary>
        public List<Card> cards { get; set; }
        /// <summary>
        /// Vehicle attached to the invoice
        /// </summary>
        public Vehicle vehicle { get; set; }
    }
}
