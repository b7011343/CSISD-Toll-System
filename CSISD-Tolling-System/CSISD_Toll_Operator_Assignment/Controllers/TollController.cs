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
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly InvoiceService _invoiceService;

        //private readonly PaymentProcessingService paymentService;

        public TollController(ILogger<TollController> logger, UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext db)
        {
            _logger = logger;
            _userManager = userManager;
            _db = db;
            _signInManager = signInManager;
            _invoiceService = new InvoiceService(_db);
            //paymentService = new PaymentProcessingService();
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = Roles.RoadUser)]
        public IActionResult Payment(long invoiceId)
        {
            Invoice invoice = _db.Invoices.Where(x => x.Id == invoiceId).First();
            List<Card> cards = _db.Cards.Where(x => x.OwnerID == _userManager.GetUserId(User)).ToList();
            Vehicle vehicle = _db.Vehicles.Where(x => x.Id == invoice.Id).First();
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
            // TODO: Add logic here for making payment
            return View();
        }
    }
}
