﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CSISD_Toll_Operator_Assignment.Models;
using Microsoft.AspNetCore.Identity;
using CSISD_Tolling_System.Models;
using System.Security.Claims;
using CSISD_Toll_Operator_Assignment.Models.View;
using CSISD_Tolling_System.Data.Service;

namespace CSISD_Toll_Operator_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<User> _userManager;
        private InvoiceService invoiceService;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
            invoiceService = new InvoiceService();
        }

        public async Task<IActionResult> Index()
        {
            string userEmail = HttpContext.User.Identity.Name;
            if (userEmail == null)
            {
                return View();
            }

            User user = await _userManager.FindByEmailAsync(userEmail);
            string userId = user.Id;
            var roleArr = await _userManager.GetRolesAsync(user);
            string role = roleArr.First();

            IndexViewModel model = new IndexViewModel() { userId = userId };

            if (role == "toll-operator")
            {
                model.invoices = invoiceService.GetAllUnpaidInvoices();
            }
            else if (role == "road-user")
            {
                model.invoices = invoiceService.GetUserUnpaidInvoices(userId);
            }

            switch(role)
            {
                case "road-user": return View("IndexRoadUser", model);
                case "toll-operator": return View("IndexTollOperator", model);
                case "admin": return View("IndexAdmin");
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
