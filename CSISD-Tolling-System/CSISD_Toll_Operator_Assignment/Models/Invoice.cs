using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSISD_Toll_Operator_Assignment.Models
{
    [Table("Invoice", Schema = "dbo")]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(19, 4)")]
        public decimal Fee { get; set; }

        [Required, StringLength(450)]
        public string UserId { get; set; }

        [Required]
        public long VehicleId { get; set; }

        [Required]
        public DateTime EntryTimestamp { get; set; }

        [Required]
        public DateTime ExitTimestamp { get; set; }

        [Required]
        public bool Paid { get; set; }
    }
}
