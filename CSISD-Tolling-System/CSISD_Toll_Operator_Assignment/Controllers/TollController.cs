using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CSISD_Toll_Operator_Assignment.Controllers
{
    public class TollController : Controller
    {
        private readonly ILogger<TollController> _logger;
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly InvoiceService invoiceService;
        private readonly PaymentProcessingService paymentService;

        public TollController(ILogger<TollController> logger, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            db = new ApplicationDbContext();
            _signInManager = signInManager;
            invoiceService = new InvoiceService();
            //paymentService = new PaymentProcessingService();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "road-user")]
        public IActionResult Payment()
        {
            Invoice invoice = db.Invoices.Where(x => x.UserId == _userManager.GetUserId(User)).First();
            List<Card> cards = db.Cards.Where(x => x.OwnerID == _userManager.GetUserId(User)).ToList();
            PaymentViewModel model = new PaymentViewModel()
            {
                invoice = invoice,
                cards = cards
            };
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "road-user")]
        public IActionResult Pay()
        {
            // TODO: Add logic here for making payment
            return View();
        }
    }
}
