using System.Collections.Generic;
using System.Linq;
using CSISD_Toll_Operator_Assignment.Service.SimulationServices;
using CSISD_Toll_Operator_Assignment.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Service;
using Microsoft.Extensions.DependencyInjection;
using CSISD_Toll_Operator_Assignment.Services.SimulationService;

namespace CSISD_Toll_Operator_Assignment.Manager
{
    public class SimulationManager
    {
        private IServiceScopeFactory _scopeFactory;

        public SimulationManager(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        /// <summary>
        /// Generate test/demo data for users, invoices, contracts, rfids and vehicles.
        ///
        /// If the database is already populated then this is a no-op.
        /// </summary>
        /// <param name="userManager">UserManager for creating users & assigning roles. Should not be null.</param>
        /// <param name="db">Application database context. Should not be null.</param>
        public void Generate()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                ApplicationDbContext db          = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                UserManager<User>    userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                if (ShouldGenerateTestData(db))
                {
                    // Need to generate users & vehicles before generating any of
                    // the others (RFIDs, invoices and contracts etc...)
                    GenerateUsersAndVehicles(userManager, db);
                    GenerateRFIDs(db);
                    GenerateInvoices(db);
                    GenerateContracts(db, userManager);
                    GenerateCards(userManager, db);
                }
            }
        }

        /// <summary>
        /// Update the database with test data for the card table
        /// </summary>
        private async void GenerateCards(UserManager<User> userManager, ApplicationDbContext db)
        {
            List<User> users = db.Users.ToList();
            List<User> roadUsers = new List<User>();
            foreach(var user in users)
            {
                if(userManager.IsInRoleAsync(user, "road-user").Result == true)
                {
                    roadUsers.Add(user);
                }
            }
            ISimulationService<Card> cardSimulator = new PaymentProcessingSimulationService(roadUsers, userManager);
            List<Card> cards = cardSimulator.GenerateAsync();
            db.AddRange(cards);
            db.SaveChanges();
        }

        /// <summary>
        /// Update the database with test data for the invoices table
        /// </summary>
        private void GenerateInvoices(ApplicationDbContext db)
        {
            ISimulationService<Invoice> invoiceSimulator = new InvoiceSimulationService(db.Vehicles);
            List<Invoice> invoices = invoiceSimulator.GenerateAsync();

            db.AddRange(invoices);
            db.SaveChanges();
        }

        /// <summary>
        /// Update the database with test data for the contracts table
        /// </summary>
        private void GenerateContracts(ApplicationDbContext db, UserManager<User> userManager)
        {
            ISimulationService<Contract> contractSimulator = new ContractSimulationService(userManager);
            List<Contract> contracts = contractSimulator.GenerateAsync();

            db.Contracts.AddRange(contracts);
            db.SaveChanges();
        }

        /// <summary>
        /// Update the database with test data for the RFID table
        /// </summary>
        private void GenerateRFIDs(ApplicationDbContext db)
        {
            ISimulationService<RFID> rfidSimulator = new RFIDSimulationService(db.Vehicles);
            List<RFID> rfids = rfidSimulator.GenerateAsync();

            db.RFIDs.AddRange(rfids);
            db.SaveChanges();
        }

        /// <summary>
        /// Update the database with test data for the users and vehicles
        /// table.
        /// Should be called before trying to generate data for RFID, contracts
        /// or invoices.
        /// </summary>
        private void GenerateUsersAndVehicles(UserManager<User> userManager, ApplicationDbContext db)
        {
            // Generate the users
            ISimulationService<User> userSimulator = new UserSimulationService(userManager);
            userSimulator.GenerateAsync();

            // Generate the vehicles (can only do this after we've generated the users)
            ISimulationService<Vehicle> vehicleSimulator = new VehicleSimulationService(db.Users);
            List<Vehicle> vehicles = vehicleSimulator.GenerateAsync();

            db.Vehicles.AddRange(vehicles);
            db.SaveChanges();
        }

        /// <summary>
        /// Determine if we should insert the test data (or not, if the database is
        /// already populated)
        /// </summary>
        /// <returns>True if the test data should be generated, false if not.</returns>
        private bool ShouldGenerateTestData(ApplicationDbContext db)
        {
            return db.Users.Count() == 0 &&
                   db.Vehicles.Count() == 0 &&
                   db.RFIDs.Count() == 0 &&
                   db.Cards.Count() == 0 &&
                   db.Contracts.Count() == 0;
        }
    }
}