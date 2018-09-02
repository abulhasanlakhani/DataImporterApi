using System;

namespace DataImporter.Business
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
