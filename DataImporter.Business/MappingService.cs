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
            var expense = expenseFromEmail;

            var costCentre = _extractor.GetXmlNodeFromElement(emailXml, DomainConstants.CostCentre);
            
            expense.CostCentre = costCentre != null ? _extractor.GetXmlNodeFromElement(emailXml, DomainConstants.CostCentre).InnerText : "UNKNOWN";
            expense.Total = Convert.ToDouble(_extractor.GetXmlNodeFromElement(emailXml, DomainConstants.Total).InnerText);
            expense.PaymentMethod = _extractor.GetXmlNodeFromElement(emailXml, DomainConstants.PaymentMethod).InnerText;
            expense.Gst = _taxCalculator.CalculateGstFromNetPrice(expense.Total, 0.15);
            expense.GrossTotal = expense.Total - expense.Gst;

            return expense;
        }
    }
}
