using CSISD_Tolling_System.Models;
using NUnit.Framework;

namespace CSISD_Tolling_System.UnitTests
{
    public class Test_Invoice
    {
        [Test]
        public void CreateInvoice_SetID()
        {
            Invoice invoice = new Invoice(1234, 0, "", 123);
            Assert.That(invoice.Id == 1234);
        }

        [Test]
        public void CreateInvoice_SetFee()
        {
            Invoice invoice = new Invoice(0, 23.50m, "", 0);
            Assert.That(invoice.Fee == 23.50m);
        }

        [Test]
        public void CreateInvoice_SetUserID()
        {
            Invoice invoice = new Invoice(0, 0, "usr1234", 0);
            Assert.That(invoice.UserId == "usr1234");
        }

        [Test]
        public void CreateInvoice_SetVehicleID()
        {
            Invoice invoice = new Invoice(0, 0, "", 4555);
            Assert.That(invoice.VehicleId == 4555);
        }
    }
}