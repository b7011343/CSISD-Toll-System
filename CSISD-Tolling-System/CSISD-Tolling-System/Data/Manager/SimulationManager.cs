using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSISD_Tolling_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSISD_Tolling_System.Data.Manager
{
    public class SimulationManager : Controller
    {
        private ApplicationDbContext db;
        private UserManager<User> userManager;

        public SimulationManager() {}


        public void generate(UserManager<User> userManager, ApplicationDbContext db)
        {
            this.db = db;
            this.userManager = userManager;
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
            List<Vehicle> invoiceVehicles = db.Vehicles.ToList();
            for (int i = 0; i < 2; i++)
            {
                Random random = new Random();
                Vehicle vehicle = invoiceVehicles[i];
                DateTime entry = DateTime.Today.AddDays(-(random.Next(3, 14)));
                DateTime exit = entry.AddHours(random.NextDouble());
                Invoice invoice = new Invoice() { Fee = 1, UserId = vehicle.OwnerID, VehicleId = vehicle.Id, Paid = false, EntryTimestamp = entry, ExitTimestamp = exit };
                db.Invoices.Add(invoice);
            }
            db.SaveChanges();
        }

        private void generateContracts()
        {

        }

        private void generateRFIDs()
        {
            List<Vehicle> vehicles = db.Vehicles.ToList();
            for (int i = 0; i < 5; i++)
            {
                if (i % 2 == 0)
                {
                    Random gen = new Random();
                    int range = 10 * 365;
                    DateTime randomExpiryDate = DateTime.Today.AddDays(gen.Next(range));
                    RFID rfid = new RFID() { RegistrationPlate = vehicles[i].RegistrationPlate, ExpiryDate = randomExpiryDate };
                    db.RFIDs.Add(rfid);
                }
            }
            db.SaveChanges();
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
                var result = await userManager.CreateAsync(user, "Test123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "road-user");
                    Vehicle vehicle = new Vehicle() { OwnerID = user.Id, Make = makes[i], Model = models[i], RegistrationPlate = regPlates[i] };
                    db.Vehicles.Add(vehicle);
                }
            }

            string adminEmail = "admin@admin.com";
            var adminUser = new User { UserName = adminEmail, Email = adminEmail, PreferenceId = 0 };
            var result2 = await userManager.CreateAsync(adminUser, "Test123!");
            if (result2.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "admin");
            }

            string tollOperatorEmail = "tolls@tolls.com";
            var tollOperatorUser = new User { UserName = tollOperatorEmail, Email = tollOperatorEmail, PreferenceId = 0 };
            var result3 = await userManager.CreateAsync(tollOperatorUser, "Test123!");
            if (result3.Succeeded)
            {
                await userManager.AddToRoleAsync(tollOperatorUser, "admin");
            }

            db.SaveChanges();
        }

        public bool dbEmpty()
        {
            return (
                db.Users.ToList().Count() == 0 &&
                db.Vehicles.ToList().Count() == 0 &&
                db.RFIDs.ToList().Count() == 0 &&
                db.Cards.ToList().Count() == 0 &&
                db.Contracts.ToList().Count() == 0
            );
        }
    }
}
