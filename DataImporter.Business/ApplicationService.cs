using DataImporter.Domain;
using System;

namespace DataImporter.Business
{
    public class ApplicationService : IApplicationService
    {
        private IExtractor _extractor;
        private ITaxCalculator _taxCalculator;

        public ApplicationService(IExtractor extractor, ITaxCalculator taxCalculator)
        {
            _extractor = extractor;
            _taxCalculator = taxCalculator;
        }
        public Response<Expense> ProcessEmailText(string emailText)
        {
            var emailXml = _extractor.ExtractXmlFromEmailText(emailText);

            var expenseFromEmail = new Expense
            {
                Total = Convert.ToDouble(_extractor.GetXmlNodeFromElement(emailXml, "total").InnerText),
                CostCentre = _extractor.GetXmlNodeFromElement(emailXml, "cost_centre").InnerText,
                PaymentMethod = _extractor.GetXmlNodeFromElement(emailXml, "payment_method").InnerText,
            };

            expenseFromEmail.Gst = _taxCalculator.CalculateGstFromNetPrice(expenseFromEmail.Total, 15);
            expenseFromEmail.GrossTotal = expenseFromEmail.Total - expenseFromEmail.Gst;

            return new Response<Expense>
            {
                Success = true,
                Payload = expenseFromEmail
            };
        }
    }
}