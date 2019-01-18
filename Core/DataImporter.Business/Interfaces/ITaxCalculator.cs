namespace DataImporter.Business.Interfaces
{
    public interface ITaxCalculator
    {
        double CalculateGstFromNetPrice(double amount, double gstPercentage);
    }
}