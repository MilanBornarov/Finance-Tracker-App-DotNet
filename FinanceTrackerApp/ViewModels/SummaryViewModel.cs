using FinanceTrackerApp.DTOs;

namespace FinanceTrackerApp.ViewModels
{
    public class SummaryViewModel
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance {  get; set; }

        public IEnumerable<TransactionDto> RecentTransactions { get; set; } = Enumerable.Empty<TransactionDto>();
        public Dictionary<string, decimal> ExpensesByCategory { get; set; } = new();
        public string BalanceClass => Balance >= 0 ? "text-success" : "text-danger";

        //Filter (month)
        public SummaryQueryDto Query { get; set; } = new();
        public List<(int Year, int Month, string Label)> AvailableMonths { get; set; } = new();
    }
}
