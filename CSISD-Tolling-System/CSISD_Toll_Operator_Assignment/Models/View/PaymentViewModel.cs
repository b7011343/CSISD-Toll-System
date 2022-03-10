using System.Collections.Generic;

namespace CSISD_Toll_Operator_Assignment.Models
{
    public class PaymentViewModel
    {
        public Invoice    invoice { get; set; }
        public List<Card> cards   { get; set; }
        public Vehicle    vehicle { get; set; }
    }
}
