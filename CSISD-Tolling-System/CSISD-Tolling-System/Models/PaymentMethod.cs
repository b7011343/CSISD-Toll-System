using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Tolling_System.Models
{
    public class PaymentMethod
    {
        public long Id { get; set; }
        [Required]
        public string PaymentType { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
    }
}
