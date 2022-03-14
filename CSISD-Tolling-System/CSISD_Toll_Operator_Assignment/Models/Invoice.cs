using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSISD_Toll_Operator_Assignment.Models
{
    [Table("Invoice", Schema = "dbo")]
    public class Invoice
    {
        /// <summary>
        /// Auto-generated Id of the invoice
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// The fee of the invoice
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(19, 4)")]
        public decimal Fee { get; set; }
        /// <summary>
        /// The UserId linked to the invoice
        /// </summary>
        [Required, StringLength(450)]
        public string UserId { get; set; }
        /// <summary>
        /// The VehicleId linked to the invoice
        /// </summary>
        [Required]
        public long VehicleId { get; set; }
<<<<<<< HEAD
        /// <summary>
        /// The EntryTimestamp on the invoice
        /// </summary>
        [Required]
        public DateTime EntryTimestamp { get; set; }
        /// <summary>
        /// The ExitTimestamp on the invoice
        /// </summary>
        [Required]
        public DateTime ExitTimestamp { get; set; }
        /// <summary>
        /// Whether or not the invoice has been paid
        /// </summary>
=======

        [Required]
        public DateTime EntryTimestamp { get; set; }

        [Required]
        public DateTime ExitTimestamp { get; set; }

>>>>>>> 7297ed2f7b6b233950b559c4a1eb682bea329f17
        [Required]
        public bool Paid { get; set; }
    }
}
