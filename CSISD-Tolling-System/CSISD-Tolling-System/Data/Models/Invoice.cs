using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Tolling_System.Models
{
    public class Invoice
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public double Fee { get; set; }
        [Required, StringLength(450)]
        public string UserId { get; set; }
        [Required]
        public int VehicleId { get; set; }
    }
}
