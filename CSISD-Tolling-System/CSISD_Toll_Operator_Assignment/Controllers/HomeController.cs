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

namespace CSISD_Toll_Operator_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly InvoiceService _invoiceService;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, ApplicationDbContext db)
        {
            _logger         = logger;
            _userManager    = userManager;
            _db             = db;
            _invoiceService = new InvoiceService(db);
        }

        public async Task<IActionResult> Index()
        {
            string userEmail = HttpContext.User.Identity.Name;

            if (userEmail == null)
                return View();

            User   user = await _userManager.FindByEmailAsync(userEmail);
            string role = (await _userManager.GetRolesAsync(user)).First();

            if (role == Roles.Administrator)
                return View("IndexAdmin");

            IndexViewModel model = new IndexViewModel(role, user, _invoiceService);

            switch(role)
            {
                case Roles.RoadUser:
                    return View("IndexRoadUser", model);

                case Roles.TollOperator:
                    return View("IndexTollOperator", model);
            }

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
    }
}
