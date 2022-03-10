using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Models.View;
using CSISD_Toll_Operator_Assignment.Service;

namespace CSISD_Toll_Operator_Assignment.Controllers
{
    public class TollController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User>    _userManager;
        private readonly InvoiceService       _invoiceService;

        public TollController(UserManager<User> userManager, ApplicationDbContext db, InvoiceService invoiceService)
        {
            _userManager    = userManager;
            _db             = db;
            _invoiceService = invoiceService;
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = Roles.RoadUser)]
        public IActionResult Payment(long invoiceId)
        {
            // Generate view model and return view
            Invoice invoice = _db.Invoices.Where(x => x.Id == invoiceId).First();
            List<Card> cards = _db.Cards.Where(x => x.OwnerID == _userManager.GetUserId(User)).ToList();
            Vehicle vehicle = _db.Vehicles.Where(x => x.Id == invoiceId).First();
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
            Invoice invoice = _db.Invoices.Where(x => x.Id == (long)Convert.ToDouble(Request.Form["id"])).First();
            Card card = _db.Cards.Where(x => x.CardNumber == (string)Request.Form["cards"].ToString()).First();
            List<Card> cards = _db.Cards.Where(x => x.OwnerID == _userManager.GetUserId(User)).ToList();
            Vehicle vehicle = _db.Vehicles.Where(x => x.Id == invoice.Id).First();
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
                _db.SaveChanges();
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
                CardNumber = model.CardNumber,
                Cvv = model.CVV,
                ExpiryDate = model.ExpiryDate,
                NameOnCard = model.NameOnCard,
                OwnerID = _userManager.GetUserId(User)
            };

            _db.Cards.Add(card);
            _db.SaveChanges();

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
            User user = await _userManager.FindByEmailAsync(userEmail);
            string role = (await _userManager.GetRolesAsync(user)).First();
            List<Contract> _tollContracts = _db.Contracts.ToList();
            var id = _userManager.GetUserId(User);
            List<Contract> _userContracts = _db.Contracts.Where(x => x.UserId == _userManager.GetUserId(User)).ToList();
            ContractViewModel tollContracts = new ContractViewModel()
            {
                Contracts = _tollContracts
            };
            ContractViewModel userContracts = new ContractViewModel()
            {
                Contracts = _userContracts
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
            Contract contract = _db.Contracts.Where(x => x.Id == contractID).First();
            return View(contract);
        }

        public async Task<IActionResult> TollHistory()
        {
            string userEmail = HttpContext.User.Identity.Name;

            if (userEmail == null)
                return View();

            // Gets current user and role
            User user = await _userManager.FindByEmailAsync(userEmail);
            string role = (await _userManager.GetRolesAsync(user)).First();
            TollHistoryViewModel model = new TollHistoryViewModel(role, user, _invoiceService);

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
