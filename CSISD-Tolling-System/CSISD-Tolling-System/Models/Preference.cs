using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Tolling_System.Models
{
    public class Preference
    {
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
