using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CSISD_Toll_Operator_Assignment.Models
{
    public class AddCardViewModel
    {
        [Required]
        [CreditCard(ErrorMessage = "Invalid card number")]
        [DisplayName("Card Number")]
        public string cardNumber { get; set; }
        
        [Required]
        [Range(100, 999, ErrorMessage = "CVV number has a length of 3 digits")]
        [DisplayName("Card CVV")]
        public int cvv { get; set; }
        
        [Required]
        [DisplayName("Card Expiry Date")]
        public DateTime expiryDate { get; set; }
        
        [Required]
        [DisplayName("Name on Card")]
        public string nameOnCard { get; set; }
    }
}
