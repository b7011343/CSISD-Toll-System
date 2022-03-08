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
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Preference defaultPreference;

        public PreferenceService(ApplicationDbContext db, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            defaultPreference = db.Preferences.Find(0L);
        }

        private Preference GetUserPreference()
        {
            User user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
            return user == null ? defaultPreference : user.GetPreference();
        }

        public string GetLanguage()
        {
            return GetUserPreference().Language;
        }

        public int GetMagnification()
        {
            return GetUserPreference().Magnification;
        }

        public bool GetColorBlindMode()
        {
            return GetUserPreference().ColorBlindMode;
        }

        public bool GetScreenReader()
        {
            return GetUserPreference().ScreenReader;
        }

        public void SetMagnification(int magnification)
        {
            Preference preference = GetUserPreference();
            preference.Magnification = magnification;
            _db.SaveChanges();
        }

        public void SetLanguage(string language)
        {
            Preference preference = GetUserPreference();
            preference.Language = language;
            _db.SaveChanges();
        }

        public void SetColorBlindMode(bool isColorBlind)
        {
            Preference preference = GetUserPreference();
            preference.ColorBlindMode = isColorBlind;
            _db.SaveChanges();
        }

        public void SetScreenReader(bool isScreenReader)
        {
            Preference preference = GetUserPreference();
            preference.ScreenReader = isScreenReader;
            _db.SaveChanges();
        }
    }
}
