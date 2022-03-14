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

        /// <summary>
        /// Adds the selected invoice (specified by the parameter invoiceId),
        /// list of cards linked to the signed in user and the vehicle linked to the invoice to the payment page
        /// </summary>
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = Roles.RoadUser)]
        public IActionResult Payment(long invoiceId)
        {
            // Get the required invoice by specifying the invoiceId from the ApplicationDbContext
            Invoice invoice = _db.Invoices.Where(x => x.Id == invoiceId).First();

            // Get the list of cards that are linked to the logged in user from the ApplicationDbContext
            List<Card> cards = _db.Cards.Where(x => x.OwnerID == _userManager.GetUserId(User)).ToList();

            // Get the vehicle linked to the invoice from the ApplicationdbContext
            Vehicle vehicle = _db.Vehicles.Where(x => x.Id == invoiceId).First();

            // Add objects above the PaymentViewModel
            PaymentViewModel model = new PaymentViewModel()
            {
                invoice = invoice,
                cards = cards,
                vehicle = vehicle
            };

            // Pass model through View()
            return View(model);
        }

        /// <summary>
        /// This method validates the CVV and allows the user to pay their invoices.
        /// </summary>
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = Roles.RoadUser)]
        public IActionResult Payment()
        {
            // Get the required invoice by specifying the invoiceId from the ApplicationdbContext
            Invoice invoice = _db.Invoices.Where(x => x.Id == (long)Convert.ToDouble(Request.Form["id"])).First();

            // Get the card selected by the user in the payment form
            Card card = _db.Cards.Where(x => x.CardNumber == (string)Request.Form["cards"].ToString()).First();

            // Get the list of cards that are linked to the logged in user from the ApplicationdbContext
            List<Card> cards = _db.Cards.Where(x => x.OwnerID == _userManager.GetUserId(User)).ToList();

            // Get the vehicle linked to the invoice from the ApplicationdbContext
            Vehicle vehicle = _db.Vehicles.Where(x => x.Id == invoice.Id).First();

            // Add objects above the PaymentViewModel
            PaymentViewModel model = new PaymentViewModel()
            {
                invoice = invoice,
                cards   = cards,
                vehicle = vehicle
            };

            int cvv = Convert.ToInt32(Request.Form["cvv"]);

            // Check if the user input CVV is the same as the CVV on the card in the database
            if(cvv == card.Cvv)
            {
                // Change boolean Paid from false to true
                invoice.Paid = true;

                // Save changes and update the context state
                _db.SaveChanges();

                // Return user to the home index page
                return LocalRedirect("/Home/Index");
            }
            else
            {
                // If the user input CVV does not match the card CVV in the database then return an error message page to the user
                return Ok("The card details you have entered are incorrect, please go back and enter them correctly.");
            }
        }

        /// <summary>
        /// This method takes user input and adds a new card
        /// </summary>
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
                Cvv        = model.CVV,
                ExpiryDate = model.ExpiryDate,
                NameOnCard = model.NameOnCard,
                OwnerID    = _userManager.GetUserId(User)
            };

            // Add card to Cards table in ApplicationDbContext, then save the changes
            _db.Cards.Add(card);
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// This method redirects the user to the AddCard view and this is only seen by the road-user
        /// the [Authorize] allows you to specify which user roles can see certain views
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "road-user")]
        public IActionResult AddCard()
        {
            // Returns AddCard view
            return View();
        }

        /// <summary>
        /// This methods shows the Contracts view and allows the road-user and toll-operator to view it
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "road-user, toll-operator")]
        public async Task<IActionResult> Contracts()
        {
            // Gets current user and checks role
            string userEmail = HttpContext.User.Identity.Name;

            User   user = await _userManager.FindByEmailAsync(userEmail);
            string role = (await _userManager.GetRolesAsync(user)).First();

            // Get list of all contracts
            List<Contract> _tollContracts = _db.Contracts.ToList();

            string id = _userManager.GetUserId(User);

            // Get list of contracts linked to the user that is signed in
            List<Contract> _userContracts = _db.Contracts.Where(x => x.UserId == _userManager.GetUserId(User)).ToList();

            // Different view rendered dependent on role
            switch (role)
            {
                case Roles.RoadUser:
                    ContractViewModel userContracts = new ContractViewModel()
                    {
                        Contracts = _userContracts
                    };

                    return View("ContractRoadUser", userContracts);

                case Roles.TollOperator:
                    ContractViewModel tollContracts = new ContractViewModel()
                    {
                        Contracts = _tollContracts
                    };

                    return View("ContractTollOperator", tollContracts);
            }

            return View();
        }

        /// <summary>
        /// This method shows the ContactDetails view
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "road-user, toll-operator")]
        public IActionResult ContractDetails(long contractID)
        {
            // Get the contract required, and return view with that contract as a model
            Contract contract = _db.Contracts.Where(x => x.Id == contractID).First();

            return View(contract);
        }

        /// <summary>
        /// This method shows the toll history view
        /// </summary>
        public async Task<IActionResult> TollHistory()
        {
            string userEmail = HttpContext.User.Identity.Name;

            if (userEmail == null)
                return View();

            // Gets current user and role
            User                 user  = await _userManager.FindByEmailAsync(userEmail);
            string               role  = (await _userManager.GetRolesAsync(user)).First();
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
