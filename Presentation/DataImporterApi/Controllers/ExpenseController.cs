using DataImporter.Business;
using DataImporter.Business.Interfaces;
using DataImporterApi.Extensions;
using DataImporterApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DataImporterApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ExpenseController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost(Name = nameof(CreateExpense))]
        [ProducesResponseType(200)]
        public IActionResult CreateExpense([FromBody] ExpenseRequestModel expenseRequest)
        {
            return Ok(_applicationService.ProcessExpenseEmailText(expenseRequest.EmailText).ToViewModel());
        }
    }
}
