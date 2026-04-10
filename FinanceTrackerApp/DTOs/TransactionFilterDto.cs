using System.ComponentModel.DataAnnotations;

namespace FinanceTrackerApp.DTOs;

public class TransactionFilterDto
{
    // Free text
    public string? Search { get; set; }

    // 'Income', 'Expense', or null 
    public string? Type { get; set; }

    // Exact category match 
    public string? Category { get; set; }

    // Date range
    [DataType(DataType.Date)]
    public DateTime? DateFrom { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateTo { get; set; }

    // Is ANY filter currently active
    public bool IsActive =>
        !string.IsNullOrWhiteSpace(Search) ||
        !string.IsNullOrWhiteSpace(Type) ||
        !string.IsNullOrWhiteSpace(Category) ||
        DateFrom.HasValue ||
        DateTo.HasValue;
}
