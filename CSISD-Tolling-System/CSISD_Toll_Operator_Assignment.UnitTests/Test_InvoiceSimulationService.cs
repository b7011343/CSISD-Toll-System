using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Service.SimulationServices;

using NUnit.Framework;
using System.Collections.Generic;

namespace CSISD_Toll_Operator_Assignment.UnitTests
{
    public class Test_InvoiceSimulationService
    {
        [Test]
        public void Test_Generate_NoVehicles_NoInvoices()
        {
            // Arrange
            Vehicle[] vehicles = new Vehicle[] { };
            ISimulationService<Invoice> sim = new InvoiceSimulationService(vehicles);

            // Act
            List<Invoice> output = sim.Generate();

            // Assert
            Assert.That(output.Count == 0);
        }

        [Test]
        public void Test_Generate_OneVehicle_OneInvoice()
        {
            Vehicle[] vehicles = new Vehicle[]
            {
                new Vehicle { Id = 1, Make = "Car", Model="Fast", RegistrationPlate="1234FG", OwnerID="bob" }
            };

            ISimulationService<Invoice> sim = new InvoiceSimulationService(vehicles);

            // Act
            List<Invoice> output = sim.Generate();

            // Assert
            Assert.That(output.Count == 1);
            Assert.That(output[0].UserId == "bob");
            Assert.That(output[0].VehicleId == 1);
            Assert.That(output[0].ExitTimestamp > output[0].EntryTimestamp);
        }

        [Test]
        public void Test_Generate_TwoVehicle_TwoInvoice()
        {
            Vehicle[] vehicles = new Vehicle[]
            {
                new Vehicle { Id = 1, Make = "Car", Model="Fast", RegistrationPlate="1234FG", OwnerID="bob"   },
                new Vehicle { Id = 2, Make = "Brr", Model="Slow", RegistrationPlate="GGFFEZ", OwnerID="harry" }
            };

            ISimulationService<Invoice> sim = new InvoiceSimulationService(vehicles);

            // Act
            List<Invoice> output = sim.Generate();

            // Assert
            Assert.That(output.Count == 2);

            Assert.That(output[0].UserId == "bob");
            Assert.That(output[0].VehicleId == 1);
            Assert.That(output[0].ExitTimestamp > output[0].EntryTimestamp);

            Assert.That(output[1].UserId == "harry");
            Assert.That(output[1].VehicleId == 2);
            Assert.That(output[1].ExitTimestamp > output[1].EntryTimestamp);
        }

    }
}