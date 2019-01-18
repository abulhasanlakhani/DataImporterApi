using DataImporter.Business;
using NUnit.Framework;
using System;
using DataImporter.Business.Services;

namespace DataImporter.Tests
{
    [TestFixture]
    public class TaxCalculatorTests
    {
        [Test]
        public void Calculate_Gst_test()
        {
            var calculator = new TaxCalculator();
            var amount = 1024.01;
            var gstPercentage = 0.15;

            var gstAmount = calculator.CalculateGstFromNetPrice(amount, gstPercentage);

            Assert.That(gstAmount, Is.GreaterThan(0));
            Assert.That(gstAmount, Is.EqualTo(133.57));
        }
    }
}
