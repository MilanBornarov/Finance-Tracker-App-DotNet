using FinanceTrackerApp.DTOs;
using FinanceTrackerApp.Helpers;
using FinanceTrackerApp.ViewModels;

namespace FinanceTrackerApp.Interfaces
{
    public interface ITransactionService
    {
        Task<PaginatedList<TransactionDto>> GetPagedAsync(
    string userId,
    int pageIndex,
    int pageSize,
    string? sortOrder = null,
    TransactionFilterDto? filter = null);

        Task<IEnumerable<TransactionDto>> GetAllAsync(string userId);

        Task<TransactionDto?> GetByIdAsync(int id, string userId);
        Task<TransactionEditDto?> GetForEditAsync(int id, string userId);
        Task CreateAsync(TransactionCreateDto transactionCreateDto, string userId);
        Task<bool> UpdateAsync(TransactionEditDto transactionEditDto, string userId);
        Task<bool> DeleteAsync(int id, string userId);
        Task<SummaryViewModel> GetSummaryAsync(string userId, SummaryQueryDto query);
        Task<IEnumerable<string>> GetCategoriesAsync(string userId);
    }
}
