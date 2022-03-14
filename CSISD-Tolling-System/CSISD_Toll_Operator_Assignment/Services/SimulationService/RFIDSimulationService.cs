using System;
using System.Collections.Generic;

using CSISD_Toll_Operator_Assignment.Models;

namespace CSISD_Toll_Operator_Assignment.Service.SimulationServices
{
    public class RFIDSimulationService : ISimulationService<RFID>
    {
        private IEnumerable<Vehicle> _vehicles;

        public RFIDSimulationService(IEnumerable<Vehicle> vehicles)
        {
            _vehicles = vehicles;
        }
        //this method creates a list of RFIDs
        public List<RFID> GenerateAsync()
        {
            int vehicleIndex = 0;

            List<RFID> rfids = new List<RFID>();
            //loop through each vehicle in IEnumerable<Vehicle>
            foreach (Vehicle vehicle in _vehicles)
            {
                if (vehicleIndex % 2 == 0)
                {
                    Random gen = new Random();
                    int range = 10 * 365;
                    //create random expiry date
                    DateTime randomExpiryDate = DateTime.Today.AddDays(gen.Next(range));
                    //create new RFID
                    RFID rfid = new RFID()
                    {
                        RegistrationPlate = vehicle.RegistrationPlate,
                        ExpiryDate = randomExpiryDate
                    };
                    //add RFID to list
                    rfids.Add(rfid);
                }
                vehicleIndex++;
            }

            return rfids;
        }
    }
}
