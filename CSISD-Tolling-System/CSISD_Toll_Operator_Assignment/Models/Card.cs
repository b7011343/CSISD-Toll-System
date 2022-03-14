using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
<<<<<<< HEAD
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
=======

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public int Cvv { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public string NameOnCard { get; set; }

>>>>>>> 7297ed2f7b6b233950b559c4a1eb682bea329f17
        [Required]
        public string OwnerID { get; set; }
    }
}
