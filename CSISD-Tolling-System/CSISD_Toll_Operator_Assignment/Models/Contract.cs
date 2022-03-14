using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSISD_Toll_Operator_Assignment.Models
{
    [Table("Contract", Schema = "dbo")]
    public class Contract
    {
        /// <summary>
        /// Auto-generated Id of the contract
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
<<<<<<< HEAD
        /// <summary>
        // LThe company name on the contract
        /// </summary>
=======

>>>>>>> 7297ed2f7b6b233950b559c4a1eb682bea329f17
        [Required]
        public string Company { get; set; }
        /// <summary>
        /// The fee of the contract
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(19, 4)")]
        public decimal Fee { get; set; }
        /// <summary>
        /// The type of contract e.g. business or personal
        /// </summary>
        [Required]
        public string Type { get; set; }
        /// <summary>
        /// The expiry date of the contract
        /// </summary>
        [Required]
        public DateTime ExpiryDate { get; set; }
        /// <summary>
        /// The userId linked to the contract
        /// </summary>
        [Required]
        public string UserId { get; set; }
    }
}
