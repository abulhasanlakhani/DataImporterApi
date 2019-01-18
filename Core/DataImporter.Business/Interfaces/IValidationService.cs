using System.Xml;
using DataImporter.Domain;
using DataImporter.Domain.Entities;
using DataImporter.Domain.Infrastructure;

namespace DataImporter.Business.Interfaces
{
    public interface IValidationService
    {
        void ValidateEmailXml(Response<Expense> response, XmlElement emailXml);
        void ValidateTotalCostInExpenseEmail(Response<Expense> response, XmlElement emailXml);
    }
}