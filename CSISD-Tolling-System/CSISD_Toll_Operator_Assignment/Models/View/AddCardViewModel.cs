using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CSISD_Toll_Operator_Assignment.Models
{
    /// <summary>
    /// Model for the AddCard.cshtml view.
    /// </summary>
    public class AddCardViewModel
    {
        /// <summary>
        /// The card number for the card thats being added.
        /// </summary>
        [Required]
        [CreditCard(ErrorMessage = "Invalid card number")]
        [DisplayName("Card Number")]
        public string CardNumber { get; set; }

        /// <summary>
        /// CVV (Card Verification Value) for the card being added.
        /// </summary>
        [Required]
        [Range(100, 999, ErrorMessage = "CVV number has a length of 3 digits")]
        [DisplayName("Card CVV")]
        public int CVV { get; set; }
        
        /// <summary>
        /// When the card being added will expire.
        /// </summary>
        [Required]
        [DisplayName("Card Expiry Date")]
        public DateTime ExpiryDate { get; set; }
        
        /// <summary>
        /// The name of the person listed on the card.
        /// </summary>
        [Required]
        [DisplayName("Name on Card")]
        public string NameOnCard { get; set; }
    }
}
