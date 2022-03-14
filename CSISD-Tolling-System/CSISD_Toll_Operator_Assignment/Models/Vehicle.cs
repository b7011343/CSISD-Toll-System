using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
<<<<<<< HEAD
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
=======

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string RegistrationPlate { get; set; }

>>>>>>> 7297ed2f7b6b233950b559c4a1eb682bea329f17
        [Required, StringLength(450)]
        public string OwnerID { get; set; }
    }
}
