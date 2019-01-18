using DataImporter.Domain;
using DataImporter.Domain.Entities;
using DataImporter.Domain.Infrastructure;
using DataImporterApi.ViewModels;

namespace DataImporterApi.Extensions
{
    public static class ExpenseMapper
    {
        public static ResponseModel<ExpenseModel> ToViewModel(this Response<Expense> response)
        {
            if (response == null) return null;

            var responseModel = new ResponseModel<ExpenseModel>
            {
                Payload = response.Payload.ToViewModel(),
                Success = response.Success,
                Validation = response.Validation.ToViewModel()
            };

            return responseModel;
        }

        public static ExpenseModel ToViewModel(this Expense expense)
        {
            if (expense == null) return null;

            return new ExpenseModel
            {
                CostCentre = expense.CostCentre,
                GrossTotal = expense.GrossTotal,
                Gst = expense.Gst,
                PaymentMethod = expense.PaymentMethod,
                Total = expense.Total
            }; 
        }

        public static ValidationModel ToViewModel(this Validation validation)
        {
            if (validation == null) return null;

            var model = new ValidationModel
            {
                ErrorMessage = validation.Message,
                ValidationType = validation.Type.ToString().ToLower()
            };

            return model;
        }
    }
}