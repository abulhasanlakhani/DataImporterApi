using System;
using DataImporter.Business.Interfaces;

namespace DataImporter.Business.Services
{
    public class TaxCalculator : ITaxCalculator
    {
        public double CalculateGstFromNetPrice(double amount, double gstPercentage)
        {
            var originalCost = amount / (1 + gstPercentage);

            var gst = amount - originalCost;

            return Math.Round(gst, 2);
        }
    }
}
