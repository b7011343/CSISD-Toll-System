using CSISD_Toll_Operator_Assignment.Models;
using System.Collections.Generic;

namespace CSISD_Toll_Operator_Assignment.Service.SimulationServices
{
    public class VehicleSimulationService : ISimulationService<Vehicle>
    {
        private IEnumerable<User> _users;

        public VehicleSimulationService(IEnumerable<User> users)
        {
            _users = users;
        }

        public List<Vehicle> GenerateAsync()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            List<string> makes = new List<string>() { "Ford", "Audi", "VW", "Skoda", "Lexus" };
            List<string> models = new List<string>() { "Modeo", "TT", "Polo", "Fabia", "IS200" };
            List<string> regPlates = new List<string>() { "MW33", "WE22", "LF90", "8008", "JB12" };

            int vehicleIndex = 0;
            foreach (User user in _users)
            {
                Vehicle vehicle = new Vehicle()
                {
                    OwnerID = user.Id,
                    Make = makes[vehicleIndex % makes.Count],
                    Model = models[vehicleIndex % models.Count],
                    RegistrationPlate = regPlates[vehicleIndex % regPlates.Count]
                };
                vehicles.Add(vehicle);
                vehicleIndex++;
            }

            return vehicles;
        }
    }
}