using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CSISD_Toll_Operator_Assignment.Models
{
    [Table("Card", Schema = "dbo")]
    public class Card
    {
        /// <summary>
        /// Auto-generated Id for Card table
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// The card number of the card being added
        /// </summary>
        [Required]
        public string CardNumber { get; set; }
        /// <summary>
        /// The Cvv of the card being added
        /// </summary>
        [Required]
        public int Cvv { get; set; }
        /// <summary>
        /// The expiry date of the card being added
        /// </summary>
        [Required]
        public DateTime ExpiryDate { get; set; }
        /// <summary>
        /// The name on the card being added
        /// </summary>
        [Required]
        public string NameOnCard { get; set; }
        /// <summary>
        /// The OwnerID of the card being added - linking the user to the card
        /// </summary>
        [Required]
        public string OwnerID { get; set; }
    }
}
