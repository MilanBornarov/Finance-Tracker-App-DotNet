using FinanceTrackerApp.DTOs;
using FinanceTrackerApp.Helpers;

namespace FinanceTrackerApp.ViewModels;

public class TransactionIndexViewModel
{
    public PaginatedList<TransactionDto> Transactions { get; set; } = null!;

    public TransactionFilterDto Filter { get; set; } = new();

    public string? CurrentSort { get; set; }

    public IEnumerable<string> AvailableCategories { get; set; }
        = Enumerable.Empty<string>();
}
