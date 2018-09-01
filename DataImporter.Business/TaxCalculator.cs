using System;

namespace DataImporter.Business
{
    public class TaxCalculator : ITaxCalculator
    {
        public double CalculateGstFromNetPrice(double amount, double gstPercentage)
        {
            //Formula taken from IRD's website
            //https://www.ird.govt.nz/gst/gst-registering/gst-about/gst-about.html

            var gst = amount * 3 / 23;
            return Math.Round(gst, 2);
        }
    }
}
