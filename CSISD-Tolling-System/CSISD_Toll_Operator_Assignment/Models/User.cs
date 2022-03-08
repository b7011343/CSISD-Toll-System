using CSISD_Toll_Operator_Assignment.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Toll_Operator_Assignment.Models
{
    public class User : IdentityUser
    {
        private readonly ApplicationDbContext _db;

        public User() {}

        public User(ApplicationDbContext db)
        {
            _db = db;
        }

        [Required]
        public long PreferenceId { get; set; }

        public Preference GetPreference()
        {
            return _db.Preferences.Find(PreferenceId);
        }
    }
}
