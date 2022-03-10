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

        public List<Invoice> GenerateAsync()
        {
            List<Invoice> invoices = new List<Invoice>();

            Random random = new Random();

            foreach (Vehicle vehicle in _vehicles)
            {
                DateTime entry = DateTime.Today.AddDays(-(random.Next(3, 14)));
                DateTime exit = entry.AddHours(random.NextDouble());

                Invoice invoice = new Invoice()
                {
                    Fee = random.Next(5, 50),
                    UserId = vehicle.OwnerID,
                    VehicleId = vehicle.Id,
                    Paid = false,
                    EntryTimestamp = entry,
                    ExitTimestamp = exit
                };

                invoices.Add(invoice);
            }

            return invoices;
        }
    }
}