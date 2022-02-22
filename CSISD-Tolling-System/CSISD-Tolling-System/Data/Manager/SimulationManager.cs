using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSISD_Tolling_System.Data.Service.SimulationServices;
using CSISD_Tolling_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSISD_Tolling_System.Data.Manager
{
    public class SimulationManager : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<User>    _userManager;

        public void generate(UserManager<User> userManager, ApplicationDbContext db)
        {
            _db          = db;
            _userManager = userManager;

            if (dbEmpty())
            {
                _generate();
            }
        }

        public void _generate()
        {
            generateUsersAndVehicles().Wait();
            generateRFIDs();
            generateInvoices();
            generateContracts();
        }

        private void generateInvoices()
        {
            ISimulationService<Invoice> invoiceSimulator = new InvoiceSimulationService(_db.Vehicles);
            List<Invoice> invoices = invoiceSimulator.Generate();

            _db.AddRange(invoices);
            _db.SaveChanges();
        }

        private void generateContracts()
        {

        }

        private void generateRFIDs()
        {
            ISimulationService<RFID> rfidSimulator = new RFIDSimulationService(_db.Vehicles);
            List<RFID> rfids = rfidSimulator.Generate();

            _db.RFIDs.AddRange(rfids);
            _db.SaveChanges();
        }

        private async Task generateUsersAndVehicles()
        {
            List<string> makes = new List<string>() { "Ford", "Audi", "VW", "Skoda", "Lexus" };
            List<string> models = new List<string>() { "Modeo", "TT", "Polo", "Fabia", "IS200" };
            List<string> regPlates = new List<string>() { "MW33", "WE22", "LF90", "8008", "JB12" };

            for (int i = 0; i < 5; i++)
            {
                string email = "test" + i + "@test.com";
                var user = new User { UserName = email, Email = email, PreferenceId = 0 };
                var result = await _userManager.CreateAsync(user, "Test123!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "road-user");
                    Vehicle vehicle = new Vehicle() { OwnerID = user.Id, Make = makes[i], Model = models[i], RegistrationPlate = regPlates[i] };
                    _db.Vehicles.Add(vehicle);
                }
            }

            string adminEmail = "admin@admin.com";
            var adminUser = new User { UserName = adminEmail, Email = adminEmail, PreferenceId = 0 };
            var result2 = await _userManager.CreateAsync(adminUser, "Test123!");
            if (result2.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, "admin");
            }

            string tollOperatorEmail = "tolls@tolls.com";
            var tollOperatorUser = new User { UserName = tollOperatorEmail, Email = tollOperatorEmail, PreferenceId = 0 };
            var result3 = await _userManager.CreateAsync(tollOperatorUser, "Test123!");
            if (result3.Succeeded)
            {
                await _userManager.AddToRoleAsync(tollOperatorUser, "toll-operator");
            }
            _db.SaveChanges();
        }

        public bool dbEmpty()
        {
            return (
                _db.Users.ToList().Count() == 0 &&
                _db.Vehicles.ToList().Count() == 0 &&
                _db.RFIDs.ToList().Count() == 0 &&
                _db.Cards.ToList().Count() == 0 &&
                _db.Contracts.ToList().Count() == 0
            );
        }
    }
}
