using CSISD_Toll_Operator_Assignment.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Toll_Operator_Assignment.Models
{
    //This inherits all variables and classes from the IdentityUser
    public class User : IdentityUser
    {
        private readonly ApplicationDbContext _db;

        public User() {}

        public User(ApplicationDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// The PreferenceId links the User table to the Preference table
        /// </summary>
        [Required]
        public long PreferenceId { get; set; }
        //get the record from Preferences that is linked to the user, by using the PreferenceId. Then store the returned data in a Preference model
        public Preference GetPreference()
        {
            return _db.Preferences.Find(PreferenceId);
        }
    }
}
