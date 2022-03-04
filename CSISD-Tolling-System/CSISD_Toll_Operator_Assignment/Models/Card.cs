using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSISD_Toll_Operator_Assignment.Models
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public int Cvv { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string NameOnCard { get; set; }
    }
}
