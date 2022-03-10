using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

        private Preference GetCurrentlyLoggedInUserPreference()
        {
            User user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
            return user == null ? _defaultPreference : _db.Preferences.Find(user.PreferenceId);
        }

        public string GetLanguage()
        {
            return GetCurrentlyLoggedInUserPreference().Language;
        }

        public int GetMagnification()
        {
            return GetCurrentlyLoggedInUserPreference().Magnification;
        }

        public bool GetColorBlindMode()
        {
            return GetCurrentlyLoggedInUserPreference().ColorBlindMode;
        }

        public bool GetScreenReader()
        {
            return GetCurrentlyLoggedInUserPreference().ScreenReader;
        }

        public void SetMagnification(int magnification)
        {
            Preference preference = GetCurrentlyLoggedInUserPreference();
            preference.Magnification = magnification;
            _db.SaveChanges();
        }

        public void SetLanguage(string language)
        {
            Preference preference = GetCurrentlyLoggedInUserPreference();
            preference.Language = language;
            _db.SaveChanges();
        }

        public void SetColorBlindMode(bool isColorBlind)
        {
            Preference preference = GetCurrentlyLoggedInUserPreference();
            preference.ColorBlindMode = isColorBlind;
            _db.SaveChanges();
        }

        public void SetScreenReader(bool isScreenReader)
        {
            Preference preference = GetCurrentlyLoggedInUserPreference();
            preference.ScreenReader = isScreenReader;
            _db.SaveChanges();
        }
    }
}
