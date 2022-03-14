using System;
using System.Collections.Generic;

using CSISD_Toll_Operator_Assignment.Models;

namespace CSISD_Toll_Operator_Assignment.Service.SimulationServices
{
    public class InvoiceSimulationService : ISimulationService<Invoice>
    {
        private IEnumerable<Vehicle> _vehicles;

        public InvoiceSimulationService(IEnumerable<Vehicle> vehicles)
        {
            _vehicles = vehicles;
        }
        //this method creates a list of invoices
        public List<Invoice> GenerateAsync()
        {
            List<Invoice> invoices = new List<Invoice>();

            Random random = new Random();

            foreach (Vehicle vehicle in _vehicles)
            {
                //create random entry and exit timestamps
                DateTime entry = DateTime.Today.AddDays(-(random.Next(3, 14)));
                DateTime exit = entry.AddHours(random.NextDouble());
                //create new invoice
                Invoice invoice = new Invoice()
                {
                    Fee = random.Next(5, 50),
                    UserId = vehicle.OwnerID,
                    VehicleId = vehicle.Id,
                    Paid = false,
                    EntryTimestamp = entry,
                    ExitTimestamp = exit
                };
                //add invoice to list
                invoices.Add(invoice);
            }

            return invoices;
        }
    }
}