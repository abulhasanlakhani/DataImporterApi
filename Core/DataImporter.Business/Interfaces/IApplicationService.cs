using DataImporter.Domain.Entities;
using DataImporter.Domain.Infrastructure;

namespace DataImporter.Business.Interfaces
{
    public interface IApplicationService
    {
        Response<Expense> ProcessExpenseEmailText(string emailText);
        Response<Reservation> ProcessReservationEmailText(string emailText);
        Response<int> CreateNewExpense(Expense expenseToCreate);
        Response<Expense> GetExpenseById(int expenseId);
    }
}