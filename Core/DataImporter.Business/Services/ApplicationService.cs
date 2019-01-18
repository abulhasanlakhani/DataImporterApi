using System;
using DataImporter.Business.Interfaces;
using DataImporter.Domain.Entities;
using DataImporter.Domain.Infrastructure;

namespace DataImporter.Business.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IExtractor _extractor;
        private readonly IValidationService _validationService;
        private readonly IMappingService _mappingService;

        public ApplicationService(IExtractor extractor, IMappingService mappingService, IValidationService validationService)
        {
            _extractor = extractor;
            _validationService = validationService;
            _mappingService = mappingService;
        }

        public Response<Expense> ProcessExpenseEmailText(string emailText)
        {
            var expenseFromEmail = new Expense();
            var response = new Response<Expense>();

            var emailXml = _extractor.ExtractXmlFromEmailText(emailText);

            _validationService.ValidateEmailXml(response, emailXml);

            if (!response.Success) return response;

            _validationService.ValidateTotalCostInExpenseEmail(response, emailXml);

            if (!response.Success) return response;

            var expense = _mappingService.MapExpenseEmailXmlToDomain(expenseFromEmail, emailXml);

            return new Response<Expense>
            {
                Payload = expense
            };
        }

        public Response<Reservation> ProcessReservationEmailText(string emailText)
        {
            var emailXml = _extractor.ExtractXmlFromEmailText(emailText);

            var reservationFromEmail = new Reservation
            {
                Vendor = _extractor.GetXmlNodeFromElement(emailXml, DomainConstants.Vendor).InnerText,
                Description = _extractor.GetXmlNodeFromElement(emailXml, DomainConstants.Description).InnerText,
                Date = Convert.ToDateTime(_extractor.GetXmlNodeFromElement(emailXml, DomainConstants.Date).InnerText)
            };

            return new Response<Reservation>
            {
                Payload = reservationFromEmail
            };
        }

        public Response<int> CreateNewExpense(Expense expenseToCreate)
        {
            var response = new Response<int>
            {
                Payload = 1
            };

            return response;
        }
    }
}