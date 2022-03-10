using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSISD_Toll_Operator_Assignment.Models
{
    [Table("PaymentMethod", Schema = "dbo")]
    public class PaymentMethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string PaymentType { get; set; }

        [Required]
        [Column(TypeName = "decimal(19, 4)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }
    }
}
