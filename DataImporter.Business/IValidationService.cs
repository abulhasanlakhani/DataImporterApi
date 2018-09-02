using System.Xml;
using DataImporter.Domain;

namespace DataImporter.Business
{
    public interface IValidationService
    {
        void ValidateEmailXml(Response<Expense> response, XmlElement emailXml);
        void ValidateTotalCostInExpenseEmail(Response<Expense> response, XmlElement emailXml);
    }
}