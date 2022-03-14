using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSISD_Toll_Operator_Assignment.Models
{
    [Table("RFID", Schema = "dbo")]
    public class RFID
    {
        /// <summary>
        /// Auto-generated Id of the RFID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// Is the RFID valid
        /// </summary>
        [Required]
        public bool IsValid { get; set; }
        /// <summary>
        /// ExpiryDate of the RFID
        /// </summary>
        [Required]
        public DateTime ExpiryDate { get; set; }
        /// <summary>
        /// The RegistrationPlate assigned to the RFID
        /// </summary>
        [Required]
        public string RegistrationPlate { get; set; }
    }
}
