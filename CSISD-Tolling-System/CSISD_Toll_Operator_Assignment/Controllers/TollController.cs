﻿using System;
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
        //this method instantiates the variables required for the controller
        public TollController(ILogger<TollController> _logger, UserManager<User> _userManager, SignInManager<User> _signInManager, ApplicationDbContext _db)
        {
            logger = _logger;
            userManager = _userManager;
            db = _db;
            signInManager = _signInManager;
            invoiceService = new InvoiceService(_db);
        }
        //this method adds the selected invoice (specified by the parameter invoiceId), list of cards linked to the signed in user and the vehicle linked to the invoice to the payment page
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = Roles.RoadUser)]
        public IActionResult Payment(long invoiceId)
        {
            // Generate view model and return view
            //get the required invoice by specifying the invoiceId from the ApplicationdbContext
            Invoice invoice = db.Invoices.Where(x => x.Id == invoiceId).First();
            //get the list of cards that are linked to the logged in user from the ApplicationdbContext
            List<Card> cards = db.Cards.Where(x => x.OwnerID == userManager.GetUserId(User)).ToList();
            //get the vehicle linked to the invoice from the ApplicationdbContext
            Vehicle vehicle = db.Vehicles.Where(x => x.Id == invoiceId).First();
            //add objects above the PaymentViewModel
            PaymentViewModel model = new PaymentViewModel()
            {
                invoice = invoice,
                cards = cards,
                vehicle = vehicle
            };
            //pass model through View()
            return View(model);
        }
        //this method validates the cvv and allows the user to pay their invoices
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = Roles.RoadUser)]
        public IActionResult Payment()
        {
            // Allows user to pay for an invoice
            //get the required invoice by specifying the invoiceId from the ApplicationdbContext
            Invoice invoice = db.Invoices.Where(x => x.Id == (long)Convert.ToDouble(Request.Form["id"])).First();
            //get the card selected by the user in the payment form
            Card card = db.Cards.Where(x => x.CardNumber == (string)Request.Form["cards"].ToString()).First();
            //get the list of cards that are linked to the logged in user from the ApplicationdbContext
            List<Card> cards = db.Cards.Where(x => x.OwnerID == userManager.GetUserId(User)).ToList();
            //get the vehicle linked to the invoice from the ApplicationdbContext
            Vehicle vehicle = db.Vehicles.Where(x => x.Id == invoice.Id).First();
            //add objects above the PaymentViewModel
            PaymentViewModel model = new PaymentViewModel()
            {
                invoice = invoice,
                cards = cards,
                vehicle = vehicle
            };
            int cvv = Convert.ToInt32(Request.Form["cvv"]);
            //cvv validation - checking if the user input cvv is the same as the cvv on the Card in the database
            if(cvv == card.Cvv)
            {
                //change boolean Paid from false to true
                invoice.Paid = true;
                //save changes and update the context state
                db.SaveChanges();
                //return user to the home index page
                return LocalRedirect("/Home/Index");
            }
            else
            {
                //if the user input cvv does not match the card cvv in the database then return an error message page to the user
                return Ok("The card details you have entered are incorrect, please go back and enter them correctly.");
            }
        }
        //this method takes user input and adds a new card
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
                OwnerID = userManager.GetUserId(User)
            };
            //add card to Cards table in ApplicationDbContext, then save the changes
            db.Cards.Add(card);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        //this method redirects the user to the AddCard view and this is only seen by the road-user
        //the [Authorize] allows you to specify which user roles can see certain views
        [HttpGet]
        [Authorize(Roles = "road-user")]
        public IActionResult AddCard()
        {
            // Returns AddCard view
            return View();
        }
        //this methods shows the Contracts view and allows the road-user and toll-operator to view it
        [HttpGet]
        [Authorize(Roles = "road-user, toll-operator")]
        public async Task<IActionResult> Contracts()
        {
            // Gets current user and checks role
            string userEmail = HttpContext.User.Identity.Name;
            User user = await userManager.FindByEmailAsync(userEmail);
            string role = (await userManager.GetRolesAsync(user)).First();
            //get list of all contracts
            List<Contract> _tollContracts = db.Contracts.ToList();
            var id = userManager.GetUserId(User);
            //get list of contracts linked to the user that is signed in
            List<Contract> _userContracts = db.Contracts.Where(x => x.UserId == userManager.GetUserId(User)).ToList();
            //create ContractViewModel for the toll operator
            ContractViewModel tollContracts = new ContractViewModel()
            {
                Contracts = _tollContracts
            };
            //create ContractViewModel for the road user
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
        //this method shows the ContactDetails view
        [HttpGet]
        [Authorize(Roles = "road-user, toll-operator")]
        public IActionResult ContractDetails(long contractID)
        {
            // Get the contract required, and return view with that contract as a model
            Contract contract = db.Contracts.Where(x => x.Id == contractID).First();
            return View(contract);
        }
        //this method shows the Tollhistory view
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
