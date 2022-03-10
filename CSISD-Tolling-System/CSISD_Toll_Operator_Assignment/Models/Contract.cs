using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSISD_Toll_Operator_Assignment.Models
{
    [Table("Contract", Schema = "dbo")]
    public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        [Column(TypeName = "decimal(19, 4)")]
        public decimal Fee { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
