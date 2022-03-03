using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace CSISD_Tolling_System.Models
{
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
    }
}
