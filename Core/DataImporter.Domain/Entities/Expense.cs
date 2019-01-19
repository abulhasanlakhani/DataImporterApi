namespace DataImporter.Domain.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public string CostCentre { get; set; }
        public double? Total { get; set; }
        public string PaymentMethod { get; set; }
        public double Gst { get; set; }
        public double GrossTotal { get; set; }
    }
}
