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
            generateUsersAndVehicles();
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

        private void generateUsersAndVehicles()
        {
            // Generate the users
            ISimulationService<User> userSimulator = new UserSimulationService(_userManager);
            userSimulator.Generate();

            // Generate the vehicles (can only do this after we've generated the users)
            ISimulationService<Vehicle> vehicleSimulator = new VehicleSimulationService(_db.Users);

            List<Vehicle> vehicles = vehicleSimulator.Generate();
            _db.Vehicles.AddRange(vehicles);

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
