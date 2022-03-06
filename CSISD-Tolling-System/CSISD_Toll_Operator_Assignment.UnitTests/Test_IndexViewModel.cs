using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Service;
using CSISD_Toll_Operator_Assignment.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSISD_Toll_Operator_Assignment.UnitTests
{
    public class Test_IndexViewModel
    {
        [Test]
        public void Test_Assigns_User_ID_For_All_Roles()
        {
            // Arrange
            var mockedUser     = new Mock<User>();
            var invoiceService = Mock.Of<IInvoiceService>();

            mockedUser.Setup(mockedUser => mockedUser.Id).Returns("1234");

            // Act
            IndexViewModel viewModel = new IndexViewModel(Roles.TollOperator, mockedUser.Object, invoiceService);

            // Assert
            Assert.AreEqual("1234", viewModel.UserId);
        }

        [Test]
        public void Test_Displays_Only_Unpaid_Invoices_For_Given_Road_User()
        {
            // Arrange
            User user = Mock.Of<User>();
            var mockedInvoiceManager = new Mock<IInvoiceService>();

            List<Invoice> unpaidInvoices = new List<Invoice>()
            {
                Mock.Of<Invoice>(),
                Mock.Of<Invoice>(),
                Mock.Of<Invoice>()
            };

            mockedInvoiceManager
                .Setup(o => o.GetUserUnpaidInvoices(It.IsAny<string>()))
                .Returns(unpaidInvoices);

            // Act
            IndexViewModel viewModel = new IndexViewModel(Roles.RoadUser, user, mockedInvoiceManager.Object);

            // Assert
            Assert.AreEqual(unpaidInvoices, viewModel.Invoices);
        }

        [Test]
        public void Test_Displays_All_Unpaid_Invoices_For_Toll_Operator()
        {
            // Arrange
            User user = Mock.Of<User>();
            var mockedInvoiceManager = new Mock<IInvoiceService>();

            List<Invoice> allInvoices = new List<Invoice>()
            {
                Mock.Of<Invoice>(),
                Mock.Of<Invoice>(),
                Mock.Of<Invoice>()
            };

            mockedInvoiceManager
                .Setup(o => o.GetAllUnpaidInvoices())
                .Returns(allInvoices);

            // Act
            IndexViewModel viewModel = new IndexViewModel(Roles.TollOperator, user, mockedInvoiceManager.Object);

            // Assert
            Assert.AreEqual(allInvoices, viewModel.Invoices);
        }
    }
}
