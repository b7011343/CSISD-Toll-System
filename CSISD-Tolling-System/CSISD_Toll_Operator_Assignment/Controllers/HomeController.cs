using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Service;
using CSISD_Toll_Operator_Assignment.Data;

namespace CSISD_Toll_Operator_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User>    _userManager;
        private readonly InvoiceService       _invoiceService;
        private readonly PreferenceService    _preferenceService;

        private const string DEFAULT_PASSWORD        = "Test123!";
        private const int    MAGNIFICATION_INCREMENT = 1;
        private long         DEFAULT_PREFERENCE_ID   = 0;

        public HomeController(UserManager<User> userManager, ApplicationDbContext db,
                              PreferenceService preferenceService, InvoiceService invoiceService)
        {
            _userManager        = userManager;
            _db                 = db;
            _invoiceService     = invoiceService;
            _preferenceService  = preferenceService;
        }

        public async Task<IActionResult> Index()
        {
            string userEmail = HttpContext.User.Identity.Name;

            if (userEmail == null)
                return View();

            // Gets the current user
            User user = await _userManager.FindByEmailAsync(userEmail);

            // Gets the current user's role
            string role = (await _userManager.GetRolesAsync(user)).First();

            // Renders different view depending on role
            switch(role)
            {
                case Roles.RoadUser:
                    return View("IndexRoadUser", new IndexViewModel(role, user, _invoiceService));

                case Roles.TollOperator:
                    return View("IndexTollOperator", new IndexViewModel(role, user, _invoiceService));

                case Roles.Administrator:
                    return View("IndexAdmin", new IndexAdminViewModel(_db.Users));
            }

            // Fallback view for user with no role
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
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
        public async Task<IActionResult> AddPrivilagedUser(string role, string email)
        {
            User user = new User()
            {
                UserName     = email,
                Email        = email,
                PreferenceId = DEFAULT_PREFERENCE_ID
            };

            // Create new user
            IdentityResult createUserResult = await _userManager.CreateAsync(user, DEFAULT_PASSWORD);

            if (!createUserResult.Succeeded)
                return RedirectToAction("Index");

            // If adding user was a success then
            IdentityResult addRoleResult = await _userManager.AddToRoleAsync(user, role);

            if (!addRoleResult.Succeeded)
                return RedirectToAction("Index");

            return RedirectToAction("Index");
        }

        public void CreateNewUserSpecificPreferenceIfNessesary()
        {
            // Checks if the current user currently has changed their preference
            User user = _userManager.GetUserAsync(User).Result;

            // If user has not changed preferences, create a new preference record in the database
            if (user.PreferenceId == DEFAULT_PREFERENCE_ID)
            {
                // The new preferences object should just be a copy for now, since the
                // user hasn't changed anything yet.
                Preference defaultPreference = _db.Preferences.Find(user.PreferenceId);
                Preference newPreference     = defaultPreference.Clone();

                // Add the new preferences row to the database, this needs to
                // be done in order to populate the ID field, which we require
                // before the next step.
                _db.Preferences.Add(newPreference);
                _db.SaveChanges();

                // Update the preference ID on the user to point to the new preferences row
                user.PreferenceId = newPreference.Id;

                _db.SaveChanges();
            }
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult IncrementMagnification(string returnUrl)
        {
            CreateNewUserSpecificPreferenceIfNessesary();

            int currentMagnification = _preferenceService.GetMagnification();
            _preferenceService.SetMagnification(currentMagnification + MAGNIFICATION_INCREMENT);

            return Redirect(returnUrl);
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult DecrementMagnification(string returnUrl)
        {
            CreateNewUserSpecificPreferenceIfNessesary();

            int currentMagnification = _preferenceService.GetMagnification();

            if (currentMagnification - MAGNIFICATION_INCREMENT >= 100)
                _preferenceService.SetMagnification(currentMagnification - MAGNIFICATION_INCREMENT);

            return Redirect(returnUrl);
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult ChangeLanguage(string returnUrl, string languages)
        {
            CreateNewUserSpecificPreferenceIfNessesary();

            _preferenceService.SetLanguage(languages);

            return Redirect(returnUrl);
        }
    }
}
