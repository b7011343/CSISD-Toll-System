using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSISD_Toll_Operator_Assignment.Models
{
    [Table("Preference", Schema = "dbo")]
    public class Preference
    {
        /// <summary>
        /// Auto-generated Id of the Preference
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
<<<<<<< HEAD
        /// <summary>
        /// The prefered FontSize of the user
        /// </summary>
        [Required]
        public int FontSize { get; set; }
        /// <summary>
        /// The prefered Language of the user
        /// </summary>
        [Required]
        public string Language { get; set; }
        /// <summary>
        /// The prefered magnification of the user
        /// </summary>
        [Required]
        public int Magnification { get; set; }
        /// <summary>
        /// The prefered value for ColorBlindMode for the user
        /// </summary>
        [Required, StringLength(5)]
        public bool ColorBlindMode { get; set; }
        /// <summary>
        /// The prefered value for ScreenReader for the user
        /// </summary>
=======

        [Required]
        public int FontSize { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public int Magnification { get; set; }

        [Required, StringLength(5)]
        public bool ColorBlindMode { get; set; }

>>>>>>> 7297ed2f7b6b233950b559c4a1eb682bea329f17
        [Required, StringLength(5)]
        public bool ScreenReader { get; set; }

        public Preference Clone()
        {
            return new Preference
            {
                FontSize       = this.FontSize,
                Magnification  = this.Magnification,
                ColorBlindMode = this.ColorBlindMode,
                ScreenReader   = this.ScreenReader,
                Language       = this.Language
            };
        }
    }
}
