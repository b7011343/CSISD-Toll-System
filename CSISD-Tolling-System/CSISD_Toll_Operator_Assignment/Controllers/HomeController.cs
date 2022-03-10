using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CSISD_Toll_Operator_Assignment.Models;
using Microsoft.AspNetCore.Identity;
using CSISD_Toll_Operator_Assignment.Service;
using CSISD_Toll_Operator_Assignment.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CSISD_Toll_Operator_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly InvoiceService _invoiceService;
        private readonly PreferenceService _preferenceService;
        private const string DEFAULT_PASSWORD = "Test123!";
        private const int MAGNIFICATION_INCREMENT = 1;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, ApplicationDbContext db, PreferenceService preferenceService)
        {
            _logger             = logger;
            _userManager        = userManager;
            _db                 = db;
            _invoiceService     = new InvoiceService(db);
            _preferenceService  = preferenceService;
        }

        public async Task<IActionResult> Index()
        {
            string userEmail = HttpContext.User.Identity.Name;

            if (userEmail == null)
                return View();

            // Gets the current user
            User   user = await _userManager.FindByEmailAsync(userEmail);
            // Gets the current user's role
            string role = (await _userManager.GetRolesAsync(user)).First();
            IndexViewModel model = new IndexViewModel(role, user, _invoiceService);

            // Renders different view depending on role
            switch(role)
            {
                case Roles.RoadUser:
                    return View("IndexRoadUser", model);

                case Roles.TollOperator:
                    return View("IndexTollOperator", model);

                case Roles.Administrator:
                    IndexAdminViewModel adminModel = new IndexAdminViewModel()
                    {
                        users = _db.Users.ToList()
                    };
                    return View("IndexAdmin", adminModel);
            }

            // Fallback view for user with no role
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddPrivilagedUser(string role, string email)
        {
            User user = new User()
            {
                UserName = email,
                Email = email,
                PreferenceId = 0
            };

            // Create new user
            Task<IdentityResult> createUserTask = _userManager.CreateAsync(user, DEFAULT_PASSWORD);
            createUserTask.Wait();
            IdentityResult result = createUserTask.Result;

            if (!result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            // If adding user was a sucess then
            Task<IdentityResult> addRoleTask = _userManager.AddToRoleAsync(user, role);
            addRoleTask.Wait();
            return RedirectToAction("Index");
        }

        public void CheckIfUserHasPreferences()
        {
            // Checks if the current user currently has changed their preference
            User user = _userManager.GetUserAsync(User).Result;

            // If user has not changed preferences, create a new preferance record in the database
            if (user.PreferenceId == 0)
            {
                Preference preference = _db.Preferences.Find(user.PreferenceId);
                Preference newPreference = new Preference()
                {
                    ColorBlindMode = preference.ColorBlindMode,
                    FontSize = preference.FontSize,
                    Magnification = preference.Magnification,
                    Language = preference.Language,
                    ScreenReader = preference.ScreenReader,
                };

                _db.Preferences.Add(newPreference);
                _db.SaveChanges();
                user.PreferenceId = newPreference.Id;
                _db.SaveChanges();
            }
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult IncrementMagnification(string returnUrl)
        {
            CheckIfUserHasPreferences();

            int currentMagnification = _preferenceService.GetMagnification();
            _preferenceService.SetMagnification(currentMagnification + MAGNIFICATION_INCREMENT);

            return Redirect(returnUrl);
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult DecrementMagnification(string returnUrl)
        {
            CheckIfUserHasPreferences();

            int currentMagnification = _preferenceService.GetMagnification();
            if (currentMagnification - MAGNIFICATION_INCREMENT >= 100)
            {
                _preferenceService.SetMagnification(currentMagnification - MAGNIFICATION_INCREMENT);
            }

            return Redirect(returnUrl);
        }
    }
}
