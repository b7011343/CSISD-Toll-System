using System;
using System.Collections.Generic;
using System.Linq;
using CSISD_Tolling_System.Models;

namespace CSISD_Tolling_System.Service.SimulationServices
{
    public class RFIDSimulationService : ISimulationService<RFID>
    {
        private IEnumerable<Vehicle> _vehicles;

        public RFIDSimulationService(IEnumerable<Vehicle> vehicles)
        {
            _vehicles = vehicles;
        }

        public List<RFID> Generate()
        {
            int vehicleIndex = 0;

            List<RFID> rfids = new List<RFID>();

            foreach (Vehicle vehicle in _vehicles)
            {
                if (vehicleIndex % 2 == 0)
                {
                    Random gen = new Random();
                    int range = 10 * 365;
                    DateTime randomExpiryDate = DateTime.Today.AddDays(gen.Next(range));

                    RFID rfid = new RFID()
                    {
                        RegistrationPlate = vehicle.RegistrationPlate,
                        ExpiryDate = randomExpiryDate
                    };

                    rfids.Add(rfid);
                }

                vehicleIndex++;
            }

            return rfids;
        }
    }
}
