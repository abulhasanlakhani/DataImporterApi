using DataImporter.Business;
using DataImporterApi.Web.Extensions;
using DataImporterApi.Web.Models;
using System.Web.Http;

namespace DataImporterApi.Web.Controllers
{
    public class ExpenseController : ApiController
    {
        private readonly IApplicationService _applicationService;

        public ExpenseController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // GET: api/Expense
        public ResponseModel<ExpenseModel> Post([FromBody]ExpenseRequestModel expenseRequest)
        {
            return _applicationService.ProcessExpenseEmailText(expenseRequest.EmailText).ToViewModel();
        }
    }
}
