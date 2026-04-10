using FinanceTrackerApp.Models;

namespace FinanceTrackerApp.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllByUserAsync(string userId);
        Task<Transaction?> GetByIdAsync(int id, string userId);
        Task AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(Transaction transaction);
        Task<bool> ExistsAsync(int id, string userId);
        IQueryable<Transaction> GetQueryableByUser(string userId);
        Task<IEnumerable<string>> GetDistinctCategoriesAsync(string userId);
    }
}

