using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CSISD_Toll_Operator_Assignment.Models
{
    public class User : IdentityUser
    {
        [Required]
        public long PreferenceId { get; set; }


    }
}
