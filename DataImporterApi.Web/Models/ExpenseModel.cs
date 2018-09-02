namespace DataImporterApi.Web.Models
{
    public class ExpenseModel
    {
        public string CostCentre { get; set; }
        public double Total { get; set; }
        public string PaymentMethod { get; set; }
        public double Gst { get; set; }
        public double GrossTotal { get; set; }
    }
}