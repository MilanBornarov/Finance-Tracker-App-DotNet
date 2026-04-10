using FinanceTrackerApp.DTOs;
using FinanceTrackerApp.Interfaces;
using FinanceTrackerApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceTrackerApp.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _service;

        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        // Get userId from JWT/cookie
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!; // just trust me !

        private const int PageSize = 10;

        // GET /Transactions
        public async Task<IActionResult> Index(
            int page = 1,
            string? sortOrder = null,
            string? search = null,
            string? type = null,
            string? category = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null)
        {
            var filter = new TransactionFilterDto
            {
                Search = search,
                Type = type,
                Category = category,
                DateFrom = dateFrom,
                DateTo = dateTo,
            };

            if (filter.IsActive && page > 1)
                page = 1;

            ViewData["DateSort"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewData["AmountSort"] = sortOrder == "amount_asc" ? "amount_desc" : "amount_asc";
            ViewData["TypeSort"] = "type";

            var vm = new TransactionIndexViewModel
            {
                Transactions = await _service.GetPagedAsync(
                    UserId, page, PageSize, sortOrder, filter),
                Filter = filter,
                CurrentSort = sortOrder,
                AvailableCategories = await _service.GetCategoriesAsync(UserId),
            };

            return View(vm);
        }


        // GET
        // /Transactions/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _service.GetByIdAsync(id, UserId);
            if (dto != null)
            {
                return View(dto);
            }
            return NotFound();
        }

        // GET
        // /Transactions/Create
        public IActionResult Create()
        {
            return View(new TransactionCreateDto
            {
                Date = DateTime.Today
            });
        }

        // POST
        // /Transactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionCreateDto dto)
        {
            if (ModelState.IsValid) {
            await _service.CreateAsync(dto, UserId);
                TempData["Success"] = "Transaction added successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET
        // /Transactions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _service.GetForEditAsync(id, UserId);
            if (dto != null) {
                return View(dto);
            } return NotFound(); 
        }

        // POST
        // /Transactions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TransactionEditDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var updated = await _service.UpdateAsync(dto, UserId);
                if (!updated)
                {
                    return NotFound();
                }
                TempData["Success"] = "Transaction updated.";
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
            }

        // GET
        // /Transactions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id, UserId);
            if (dto != null)
            {
                return View(dto);
            }
            return NotFound();
            }

        // POST
        // /Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id, UserId);
            TempData["Success"] = "Transaction deleted.";
            return RedirectToAction(nameof(Index));
        }

    }
}
