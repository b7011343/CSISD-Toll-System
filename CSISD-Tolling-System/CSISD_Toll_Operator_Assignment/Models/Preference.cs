using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace CSISD_Toll_Operator_Assignment.Models
{
    [Table("Preference", Schema = "dbo")]
    public class Preference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public int FontSize { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public int Magnification { get; set; }
        [Required, StringLength(5)]
        public string ColorBlindMode { get; set; }
        [Required, StringLength(5)]
        public string ScreenReader { get; set; }
    }
}
