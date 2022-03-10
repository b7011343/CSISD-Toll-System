using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSISD_Toll_Operator_Assignment.Models
{
    [Table("RFID", Schema = "dbo")]
    public class RFID
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public bool IsValid { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public string RegistrationPlate { get; set; }
    }
}
