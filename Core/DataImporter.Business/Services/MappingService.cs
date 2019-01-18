using System;
using System.Xml;
using DataImporter.Business.Interfaces;
using DataImporter.Domain;

namespace DataImporter.Business.Services
{
    public class MappingService : IMappingService
    {
        private readonly IExtractor _extractor;
        private readonly ITaxCalculator _taxCalculator;

        public MappingService(IExtractor extractor, ITaxCalculator taxCalculator)
        {
            _extractor = extractor;
            _taxCalculator = taxCalculator;
        }

        public Expense MapExpenseEmailXmlToDomain(Expense expenseFromEmail, XmlElement emailXml)
        {
            var costCentreNode = _extractor.GetXmlNodeFromElement(emailXml, DomainConstants.CostCentre);
            var totalNode = _extractor.GetXmlNodeFromElement(emailXml, DomainConstants.Total);
            var paymentMethodNode = _extractor.GetXmlNodeFromElement(emailXml, DomainConstants.PaymentMethod);

            expenseFromEmail.CostCentre = costCentreNode != null ? costCentreNode.InnerText : "UNKNOWN";
            expenseFromEmail.Total = Convert.ToDouble(totalNode.InnerText);
            expenseFromEmail.PaymentMethod = paymentMethodNode.InnerText;
            expenseFromEmail.Gst = _taxCalculator.CalculateGstFromNetPrice(expenseFromEmail.Total, 0.15);
            expenseFromEmail.GrossTotal = expenseFromEmail.Total - expenseFromEmail.Gst;

            return expenseFromEmail;
        }
    }
}
