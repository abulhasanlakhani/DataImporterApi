namespace DataImporter.Business
{
    public interface ITaxCalculator
    {
        double CalculateGstFromNetPrice(double amount, double gstPercentage);
    }
}