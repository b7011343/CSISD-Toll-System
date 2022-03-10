using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Service.SimulationServices;

using NUnit.Framework;
using System.Collections.Generic;

namespace CSISD_Toll_Operator_Assignment.UnitTests
{
    public class Test_RFIDSimulationService
    {
        [Test]
        public void Test_Generate_NoInputVehicles_CreatesNoRFIDs()
        {
            // Arrange
            Vehicle[] vehicles = new Vehicle[] { };
            ISimulationService<RFID> sim = new RFIDSimulationService(vehicles);

            // Act
            List<RFID> output = sim.GenerateAsync();

            // Assert
            Assert.That(output.Count == 0);
        }

        [Test]
        public void Test_Generate_OneInputVehicle_CreatesOneRFID()
        {
            // Arrange
            Vehicle[] vehicles = new Vehicle[]
            {
                new Vehicle { Id = 1, Make = "Car", Model="Fast", RegistrationPlate="1234FG", OwnerID="bob" }
            };

            ISimulationService<RFID> sim = new RFIDSimulationService(vehicles);

            // Act
            List<RFID> output = sim.GenerateAsync();

            // Assert
            Assert.That(output.Count == 1);
            Assert.That(output[0].RegistrationPlate == "1234FG");
        }

        [Test]
        public void Test_Generate_TwoInputVehicles_CreatesOneRFID()
        {
            // Arrange
            Vehicle[] vehicles = new Vehicle[]
            {
                new Vehicle { Id = 1, Make = "Car", Model="Fast", RegistrationPlate="1234FG", OwnerID="bob"   },
                new Vehicle { Id = 2, Make = "Brr", Model="Slow", RegistrationPlate="GGFFEZ", OwnerID="harry" }
            };

            ISimulationService<RFID> sim = new RFIDSimulationService(vehicles);

            // Act
            List<RFID> output = sim.GenerateAsync();

            // Assert
            Assert.That(output.Count == 1);
            Assert.That(output[0].RegistrationPlate == "1234FG");
        }

        [Test]
        public void Test_Generate_ThreeInputVehicles_CreatesTwoRFID()
        {
            // Arrange
            Vehicle[] vehicles = new Vehicle[]
            {
                new Vehicle { Id = 1, Make = "Car", Model="Fast", RegistrationPlate="1234FG", OwnerID="bob"   },
                new Vehicle { Id = 2, Make = "Slo", Model="Gone", RegistrationPlate="NOTME1", OwnerID="loser" },
                new Vehicle { Id = 3, Make = "Brr", Model="Slow", RegistrationPlate="GGFFEZ", OwnerID="harry" }
            };

            ISimulationService<RFID> sim = new RFIDSimulationService(vehicles);

            // Act
            List<RFID> output = sim.GenerateAsync();

            // Assert
            Assert.That(output.Count == 2);
            Assert.That(output[0].RegistrationPlate == "1234FG");
            Assert.That(output[1].RegistrationPlate == "GGFFEZ");
        }
    }
}