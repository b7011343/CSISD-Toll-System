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

namespace CSISD_Toll_Operator_Assignment.Controllers
{
    public class TollController : Controller
    {
        private readonly ILogger<TollController> _logger;
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> _userManager;
        private readonly InvoiceService invoiceService;

        public TollController(ILogger<TollController> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
            db = new ApplicationDbContext();
            invoiceService = new InvoiceService();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "road-user")]
        public IActionResult Payment(long invoiceId)
        {
            Invoice invoice = db.Invoices.Where(x => x.Id == invoiceId).First();
            PaymentViewModel model = new PaymentViewModel()
            {
                invoice = invoice
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
