using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Domain
{
    public class Expense
    {
        public string CostCentre { get; set; }
        public double Total { get; set; }
        public string PaymentMethod { get; set; }
        public double Gst { get; set; }
        public double GrossTotal { get; set; }
    }
}
