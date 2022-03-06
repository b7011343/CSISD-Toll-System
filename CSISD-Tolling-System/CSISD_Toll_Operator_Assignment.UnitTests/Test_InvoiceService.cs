using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Service;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSISD_Toll_Operator_Assignment.UnitTests
{
    class Test_InvoiceService
    {
        private Invoice[] _invoices = new Invoice[]
        {
            new Invoice() { Id = 0, UserId = "0", Fee = 100, VehicleId = 0, Paid = false },
            new Invoice() { Id = 1, UserId = "0", Fee = 160, VehicleId = 0, Paid = false },
            new Invoice() { Id = 2, UserId = "1", Fee = 20,  VehicleId = 1, Paid = true  },
            new Invoice() { Id = 3, UserId = "2", Fee = 30,  VehicleId = 2, Paid = false },
            new Invoice() { Id = 4, UserId = "2", Fee = 50,  VehicleId = 2, Paid = true },
            new Invoice() { Id = 5, UserId = "3", Fee = 500, VehicleId = 3, Paid = true  }
        };

        private IApplicationDbContext _databaseContext;

        [OneTimeSetUp]
        public void Setup()
        {
            Mock<IApplicationDbContext> mock = new Mock<IApplicationDbContext>();
            mock.Setup(m => m.Invoices).ReturnsDbSet(_invoices);

            _databaseContext = mock.Object;
        }

        [Test]
        public void Test_Get_All_User_Invoices_For_User_That_Exists()
        {
            // Arrange
            List<Invoice> expected = new List<Invoice>() { _invoices[0], _invoices[1] };

            // Act
            InvoiceService service = new InvoiceService(_databaseContext);
            List<Invoice> actual = service.GetUserInvoices("0");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Get_All_Unpaid_User_Invoices_For_User_That_Exists()
        {
            // Arrange
            List<Invoice> expected = new List<Invoice>() { _invoices[3], };

            // Act
            InvoiceService service = new InvoiceService(_databaseContext);
            List<Invoice> actual = service.GetUserUnpaidInvoices("2");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Get_All_Invoices()
        {
            // Arrange
            // Act
            InvoiceService service = new InvoiceService(_databaseContext);
            List<Invoice> actual = service.GetAllInvoices();

            // Assert
            Assert.AreEqual(_invoices, actual);
        }

        [Test]
        public void Test_Get_All_Unpaid_Invoices()
        {
            // Arrange
            List<Invoice> expected = new List<Invoice>() { _invoices[0], _invoices[1], _invoices[3] };

            // Act
            InvoiceService service = new InvoiceService(_databaseContext);
            List<Invoice> actual = service.GetAllUnpaidInvoices();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
