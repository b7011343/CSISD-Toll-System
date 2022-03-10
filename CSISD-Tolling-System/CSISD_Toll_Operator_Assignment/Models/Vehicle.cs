using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSISD_Toll_Operator_Assignment.Models
{
    [Table("Vehicle", Schema = "dbo")]
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
