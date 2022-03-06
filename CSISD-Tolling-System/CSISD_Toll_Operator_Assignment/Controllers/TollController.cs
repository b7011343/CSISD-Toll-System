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
        private readonly ILogger<TollController> logger;
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly InvoiceService invoiceService;
        private readonly PaymentProcessingSimulationService paymentService;

        //private readonly PaymentProcessingService paymentService;

        public TollController(ILogger<TollController> _logger, UserManager<User> _userManager, SignInManager<User> _signInManager, ApplicationDbContext _db)
        {
            logger = _logger;
            userManager = _userManager;
            db = _db;
            signInManager = _signInManager;
            invoiceService = new InvoiceService(_db);
            //paymentService = new PaymentProcessingService();
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = Roles.RoadUser)]
        public IActionResult Payment(long invoiceId)
        {
            Invoice invoice = db.Invoices.Where(x => x.Id == invoiceId).First();
            List<Card> cards = db.Cards.Where(x => x.OwnerID == userManager.GetUserId(User)).ToList();
            Vehicle vehicle = db.Vehicles.Where(x => x.Id == invoiceId).First();
            PaymentViewModel model = new PaymentViewModel()
            {
                invoice = invoice,
                cards = cards,
                vehicle = vehicle
            };
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = Roles.RoadUser)]
        public IActionResult Pay()
        {
            Invoice invoice = db.Invoices.Where(x => x.Id == (long)Convert.ToDouble(Request.Form["id"])).First();
            invoice.Paid = true;
            db.SaveChanges();
            return LocalRedirect("/Home/Index");
        }
    }
}
