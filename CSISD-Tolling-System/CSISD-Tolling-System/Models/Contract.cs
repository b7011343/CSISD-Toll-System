using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Tolling_System.Models
{
    public class Contract
    {
        public long Id { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public double Fee { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
    }
}
