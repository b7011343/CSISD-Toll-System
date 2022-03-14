using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Models;

namespace CSISD_Toll_Operator_Assignment.Service
{
    public class PreferenceService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User>    _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Preference                    _defaultPreference;

        public PreferenceService(ApplicationDbContext db, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _db                  = db;
            _userManager         = userManager;
            _httpContextAccessor = httpContextAccessor;
            _defaultPreference   = db.Preferences.Find(0L);
        }
<<<<<<< HEAD
        //this method gets the user Preference settings
        private Preference GetUserPreference()
=======

        private Preference GetCurrentlyLoggedInUserPreference()
>>>>>>> 7297ed2f7b6b233950b559c4a1eb682bea329f17
        {
            User user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
            return user == null ? _defaultPreference : _db.Preferences.Find(user.PreferenceId);
        }
        //this method gets the users prefered language
        public string GetLanguage()
        {
            return GetCurrentlyLoggedInUserPreference().Language;
        }
        //this method get the user prefered magnification value
        public int GetMagnification()
        {
            return GetCurrentlyLoggedInUserPreference().Magnification;
        }
        //this method gets if the user is color blind 
        public bool GetColorBlindMode()
        {
            return GetCurrentlyLoggedInUserPreference().ColorBlindMode;
        }
        //this method gets if the user requires a screen reader
        public bool GetScreenReader()
        {
            return GetCurrentlyLoggedInUserPreference().ScreenReader;
        }
        //this method sets the users prefered magnifcation value
        public void SetMagnification(int magnification)
        {
            Preference preference = GetCurrentlyLoggedInUserPreference();
            preference.Magnification = magnification;
            _db.SaveChanges();
        }
        //this method sets the users prefered language
        public void SetLanguage(string language)
        {
            Preference preference = GetCurrentlyLoggedInUserPreference();
            preference.Language = language;
            _db.SaveChanges();
        }
        //this method sets the users prefered option for color blind mode
        public void SetColorBlindMode(bool isColorBlind)
        {
            Preference preference = GetCurrentlyLoggedInUserPreference();
            preference.ColorBlindMode = isColorBlind;
            _db.SaveChanges();
        }
        //this method sets the users prefered option for screen reader mode
        public void SetScreenReader(bool isScreenReader)
        {
            Preference preference = GetCurrentlyLoggedInUserPreference();
            preference.ScreenReader = isScreenReader;
            _db.SaveChanges();
        }
    }
}
