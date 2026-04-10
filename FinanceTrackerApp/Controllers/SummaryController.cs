using FinanceTrackerApp.DTOs;
using FinanceTrackerApp.Interfaces;
using FinanceTrackerApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceTrackerApp.Controllers
{
    [Authorize]
    public class SummaryController : Controller
    {
        private readonly ITransactionService _service;
        public SummaryController(ITransactionService service) => _service = service;
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        // GET /Summary?month=3&year=2025
        public async Task<IActionResult> Index(int? month, int? year)
        {
            var query = new SummaryQueryDto { Month = month, Year = year };
            var vm = await _service.GetSummaryAsync(UserId, query);
            return View(vm);
        }

    }
}
