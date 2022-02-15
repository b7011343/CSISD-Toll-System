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

        public SimulationManager()
        {
        }

        public void generate(UserManager<User> userManager, ApplicationDbContext db)
        {
            this.db = db;
            this.userManager = userManager;
            cleanup();
            _generate();
        }

        public async void _generate()
        {
            generateUsersAndVehicles().Wait();
            generateRFIDs();
            generateInvoices();
            generateContracts();
        }

        private void generateInvoices()
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

        private void generateContracts()
        {

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
                    Vehicle vehicle = new Vehicle() { OwnerID = user.Id, Make = makes[i], Model = models[i], RegistrationPlate = regPlates[i] };
                    db.Vehicles.Add(vehicle);
                }
            }
            db.SaveChanges();
        }

        private void cleanup()
        {
            db.Users.RemoveRange(db.Users);
            db.RFIDs.RemoveRange(db.RFIDs);
            db.Vehicles.RemoveRange(db.Vehicles);
            db.SaveChanges();
        }
    }
}
