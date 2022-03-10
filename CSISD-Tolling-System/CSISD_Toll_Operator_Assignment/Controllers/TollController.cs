using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Models.View;
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

        public TollController(ILogger<TollController> _logger, UserManager<User> _userManager, SignInManager<User> _signInManager, ApplicationDbContext _db)
        {
            logger = _logger;
            userManager = _userManager;
            db = _db;
            signInManager = _signInManager;
            invoiceService = new InvoiceService(_db);
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = Roles.RoadUser)]
        public IActionResult Payment(long invoiceId)
        {
            // Generate view model and return view
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
        public IActionResult Payment()
        {
            // Allows user to pay for an invoice
            Invoice invoice = db.Invoices.Where(x => x.Id == (long)Convert.ToDouble(Request.Form["id"])).First();
            Card card = db.Cards.Where(x => x.CardNumber == (string)Request.Form["cards"].ToString()).First();
            List<Card> cards = db.Cards.Where(x => x.OwnerID == userManager.GetUserId(User)).ToList();
            Vehicle vehicle = db.Vehicles.Where(x => x.Id == invoice.Id).First();
            PaymentViewModel model = new PaymentViewModel()
            {
                invoice = invoice,
                cards = cards,
                vehicle = vehicle
            };
            int cvv = Convert.ToInt32(Request.Form["cvv"]);
            if(cvv == card.Cvv)
            {
                invoice.Paid = true;
                db.SaveChanges();
                return LocalRedirect("/Home/Index");
            }
            else
            {
                return Ok("The card details you have entered are incorrect, please go back and enter them correctly.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "road-user")]
        [ValidateAntiForgeryToken]
        public IActionResult AddCard(AddCardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Creates new card in the database with the current users ID on it
            Card card = new Card()
            {
                CardNumber = model.cardNumber,
                Cvv = model.cvv,
                ExpiryDate = model.expiryDate,
                NameOnCard = model.nameOnCard,
                OwnerID = userManager.GetUserId(User)
            };

            db.Cards.Add(card);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "road-user")]
        public IActionResult AddCard()
        {
            // Returns AddCard view
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "road-user, toll-operator")]
        public async Task<IActionResult> Contracts()
        {
            // Gets current user and checks role
            string userEmail = HttpContext.User.Identity.Name;
            User user = await userManager.FindByEmailAsync(userEmail);
            string role = (await userManager.GetRolesAsync(user)).First();
            List<Contract> _tollContracts = db.Contracts.ToList();
            var id = userManager.GetUserId(User);
            List<Contract> _userContracts = db.Contracts.Where(x => x.UserId == userManager.GetUserId(User)).ToList();
            ContractView tollContracts = new ContractView()
            {
                contracts = _tollContracts
            };
            ContractView userContracts = new ContractView()
            {
                contracts = _userContracts
            };
            // Different view rendered dependent on role
            switch (role)
            {
                case Roles.RoadUser:
                    return View("ContractRoadUser", userContracts);
                case Roles.TollOperator:
                    return View("ContractTollOperator", tollContracts);
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "road-user, toll-operator")]
        public IActionResult ContractDetails(long contractID)
        {
            // Get the contract required, and return view with that contract as a model
            Contract contract = db.Contracts.Where(x => x.Id == contractID).First();
            return View(contract);
        }

        public async Task<IActionResult> TollHistory()
        {
            string userEmail = HttpContext.User.Identity.Name;

            if (userEmail == null)
                return View();

            // Gets current user and role
            User user = await userManager.FindByEmailAsync(userEmail);
            string role = (await userManager.GetRolesAsync(user)).First();
            TollHistoryViewModel model = new TollHistoryViewModel(role, user, invoiceService);

            // Renders different view dependent on roll for toll history
            switch (role)
            {
                case Roles.RoadUser:
                    return View("TollHistoryRoadUser", model);

                case Roles.TollOperator:
                    return View("TollHistoryTollOperator", model);
            }

            return View();
        }
    }
}
