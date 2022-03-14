using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSISD_Toll_Operator_Assignment.Models
{
    [Table("PaymentMethod", Schema = "dbo")]
    public class PaymentMethod
    {
        /// <summary>
        /// Auto-generated Id of the PaymentMethod
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// The PaymentType of the PaymentMethod
        /// </summary>
        [Required]
        public string PaymentType { get; set; }
        /// <summary>
        /// The Amount paid
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(19, 4)")]
        public decimal Amount { get; set; }
        /// <summary>
        /// The Timestamp of the payment
        /// </summary>
        [Required]
        public DateTime TimeStamp { get; set; }
    }
}
