using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSISD_Tolling_System.Models
{
    public class Invoice
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(19, 4)")]
        public decimal Fee { get; set; }

        [Required, StringLength(450)]
        public string UserId { get; set; }

        [Required]
        public int VehicleId { get; set; }
    }
}
