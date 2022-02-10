using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Tolling_System.Models
{
    public class Vehicle
    {
        public long Id { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string RegistrationPlate { get; set; }
        [Required, StringLength(450)]
        public string OwnerID { get; set; }
    }
}
