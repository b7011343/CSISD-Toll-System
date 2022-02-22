using System;
using System.Collections.Generic;
using System.Linq;
using CSISD_Tolling_System.Models;

namespace CSISD_Tolling_System.Data.Service.SimulationServices
{
    public class InvoiceSimulationService : ISimulationService<Invoice>
    {
        private IEnumerable<Vehicle> _vehicles;

        public InvoiceSimulationService(IEnumerable<Vehicle> vehicles)
        {
            _vehicles = vehicles;
        }

        public List<Invoice> Generate()
        {
            List<Invoice> invoices = new List<Invoice>();

            Random random = new Random();

            foreach (Vehicle vehicle in _vehicles.Take(2))
            {
                DateTime entry = DateTime.Today.AddDays(-(random.Next(3, 14)));
                DateTime exit = entry.AddHours(random.NextDouble());

                Invoice invoice = new Invoice()
                {
                    Fee = 1,
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
