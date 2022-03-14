using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Toll_Operator_Assignment.Models
{
    [Table("Vehicle", Schema = "dbo")]
    public class Vehicle
    {
        /// <summary>
        /// Auto-generated Id for the Vehicle
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// The Make of the Vehicle
        /// </summary>
        [Required]
        public string Make { get; set; }
        /// <summary>
        /// The Model of the Vehicle
        /// </summary>
        [Required]
        public string Model { get; set; }
        /// <summary>
        /// The RegistrationPlate of the Vehicle
        /// </summary>
        [Required]
        public string RegistrationPlate { get; set; }
        /// <summary>
        /// The OwnerID of the Vehicle
        /// </summary>
        [Required, StringLength(450)]
        public string OwnerID { get; set; }
    }
}
