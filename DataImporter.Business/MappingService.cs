using DataImporter.Domain;
using System;
using System.Xml;

namespace DataImporter.Business
{
    public class MappingService : IMappingService
    {
        private IExtractor _extractor;
        private ITaxCalculator _taxCalculator;

        public MappingService(IExtractor extractor, ITaxCalculator taxCalculator)
        {
            _extractor = extractor;
            _taxCalculator = taxCalculator;
        }

        public Expense MapExpenseEmailXmlToDomain(Expense expenseFromEmail, XmlElement emailXml)
        {
            var costCentre = _extractor.GetXmlNodeFromElement(emailXml, DomainConstants.CostCentre);

            expenseFromEmail.CostCentre = costCentre != null ? _extractor.GetXmlNodeFromElement(emailXml, DomainConstants.CostCentre).InnerText : "UNKNOWN";
            expenseFromEmail.Total = Convert.ToDouble(_extractor.GetXmlNodeFromElement(emailXml, DomainConstants.Total).InnerText);
            expenseFromEmail.PaymentMethod = _extractor.GetXmlNodeFromElement(emailXml, DomainConstants.PaymentMethod).InnerText;
            expenseFromEmail.Gst = _taxCalculator.CalculateGstFromNetPrice(expenseFromEmail.Total, 0.15);
            expenseFromEmail.GrossTotal = expenseFromEmail.Total - expenseFromEmail.Gst;

            return expenseFromEmail;
        }
    }
}
