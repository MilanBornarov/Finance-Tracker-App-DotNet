using FinanceTrackerApp.Data;
using FinanceTrackerApp.Interfaces;
using FinanceTrackerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerApp.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllByUserAsync(string userId)
            => await _context.Transactions.Where(i => i.UserId == userId)
            .OrderByDescending(i => i.Date)
            .AsNoTracking()
            .ToListAsync();

        public async Task<Transaction?> GetByIdAsync(int id, string userId)
            => await _context.Transactions
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

        public async Task AddAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id, string userId)
            => await _context.Transactions
            .AnyAsync(i => i.Id.Equals(id) && i.UserId.Equals(userId));

        public IQueryable<Transaction> GetQueryableByUser(string userId)
            => _context.Transactions
                .Where(t => t.UserId == userId)
                .AsNoTracking();

        public async Task<IEnumerable<string>> GetDistinctCategoriesAsync(string userId)
            => await _context.Transactions
                   .Where(t => t.UserId == userId)
                   .Select(t => t.Category)
                   .Distinct()
                   .OrderBy(c => c)
                   .AsNoTracking()
                   .ToListAsync();

    }

}
