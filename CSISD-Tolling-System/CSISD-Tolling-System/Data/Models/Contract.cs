using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSISD_Tolling_System.Models
{
    public class Contract
    {
        [Key]
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
    }
}
