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
        [Required]
        public string OwnerID { get; set; }
    }
}
