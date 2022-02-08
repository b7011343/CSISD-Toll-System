using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Tolling_System.Models
{
    public class RFID
    {
        public int Id { get; set; }
        [Required, StringLength(5)]
        public string IsValid { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string RegistrationPlate { get; set; }
    }
}
