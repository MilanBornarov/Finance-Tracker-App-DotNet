using AutoMapper;
using FinanceTrackerApp.DTOs;
using FinanceTrackerApp.Helpers;
using FinanceTrackerApp.Interfaces;
using FinanceTrackerApp.Models;
using FinanceTrackerApp.Repositories;
using FinanceTrackerApp.ViewModels;

namespace FinanceTrackerApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;
        private readonly IMapper _mapper;
        public TransactionService(ITransactionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task CreateAsync(TransactionCreateDto transactionCreateDto, string userId)
        {
            var transaction = _mapper.Map<Transaction>(transactionCreateDto);
            transaction.UserId = userId;
            await _repo.AddAsync(transaction);
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var existing = await _repo.GetByIdAsync(id, userId);
            if (existing != null)
            {
                await _repo.DeleteAsync(existing);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllAsync(string userId)
        {
            var transactions = await _repo.GetAllByUserAsync(userId);
            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public async Task<TransactionDto?> GetByIdAsync(int id, string userId)
        {
            var transaction = await _repo.GetByIdAsync(id, userId);
            if(transaction != null)
            {
                return _mapper.Map<TransactionDto>(transaction); 
            }
            return null;
        }

        public async Task<TransactionEditDto?> GetForEditAsync(int id, string userId)
        {
            var transaction = await _repo.GetByIdAsync(id, userId);
            if(transaction != null)
            {
                return _mapper.Map<TransactionEditDto>(transaction);
            }
            return null;
        }

        public async Task<PaginatedList<TransactionDto>> GetPagedAsync(
    string userId,
    int pageIndex,
    int pageSize,
    string? sortOrder = null,
    TransactionFilterDto? filter = null)
        {
            var query = _repo.GetQueryableByUser(userId);

            if (filter is not null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Search))
                {
                    var term = filter.Search.Trim().ToLower();
                    query = query.Where(t =>
                        t.Description.ToLower().Contains(term) ||
                        t.Category.ToLower().Contains(term));
                }

                // Type filter (exact match)
                if (!string.IsNullOrWhiteSpace(filter.Type))
                    query = query.Where(t => t.Type == filter.Type);

                // Category filter (exact match)
                if (!string.IsNullOrWhiteSpace(filter.Category))
                    query = query.Where(t => t.Category == filter.Category);

                // Date range
                if (filter.DateFrom.HasValue)
                    query = query.Where(t => t.Date >= filter.DateFrom.Value.Date);

                if (filter.DateTo.HasValue)
                    query = query.Where(t => t.Date <= filter.DateTo.Value.Date
                                                        .AddDays(1).AddTicks(-1));
            }

            query = sortOrder switch
            {
                "date_asc" => query.OrderBy(t => t.Date),
                "amount_desc" => query.OrderByDescending(t => t.Amount),
                "amount_asc" => query.OrderBy(t => t.Amount),
                "type" => query.OrderBy(t => t.Type),
                _ => query.OrderByDescending(t => t.Date),
            };

            var dtoQuery = query.Select(t => new TransactionDto
            {
                Id = t.Id,
                Description = t.Description,
                Amount = t.Amount,
                Type = t.Type,
                Category = t.Category,
                Date = t.Date,
                Notes = t.Notes,
            });

            return await PaginatedList<TransactionDto>.CreateAsync(dtoQuery, pageIndex, pageSize);
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync(string userId)
    => await _repo.GetDistinctCategoriesAsync(userId);



        public async Task<(decimal TotalIncome, decimal TotalExpenses, decimal Balance)> GetSummaryAsync(string userId)
        {
            var transactions = await _repo.GetAllByUserAsync(userId);
            var income = transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);
            var expenses = transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);
            return (income, expenses, income-expenses);
        }

        public async Task<SummaryViewModel> GetSummaryAsync(string userId, SummaryQueryDto query)
        {
            var allTransactions = await _repo.GetAllByUserAsync(userId);

            var availableMonths = allTransactions
                .GroupBy(t => new { t.Date.Year, t.Date.Month })
                .OrderByDescending(g => g.Key.Year).ThenByDescending(g => g.Key.Month)
                .Select(g => (
                    Year: g.Key.Year,
                    Month: g.Key.Month,
                    Label: new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM yyyy")
                ))
                .ToList();

            var filtered = query.HasFilter
                ? allTransactions.Where(t =>
                    t.Date.Month == query.Month!.Value &&
                    t.Date.Year == query.Year!.Value)
                : allTransactions;

            var income = filtered.Where(t => t.Type == "Income").Sum(t => t.Amount);
            var expenses = filtered.Where(t => t.Type == "Expense").Sum(t => t.Amount);

            return new SummaryViewModel
            {
                TotalIncome = income,
                TotalExpense = expenses,
                Balance = income - expenses,
                Query = query,
                AvailableMonths = availableMonths,
                RecentTransactions = _mapper.Map<IEnumerable<TransactionDto>>(
                    filtered.OrderByDescending(t => t.Date).Take(10)),
                ExpensesByCategory = filtered
                    .Where(t => t.Type == "Expense")
                    .GroupBy(t => t.Category)
                    .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount)),
            };
        }


        public async Task<bool> UpdateAsync(TransactionEditDto transactionEditDto, string userId)
        {
            var existing = await _repo.GetByIdAsync(transactionEditDto.Id, userId);
            if (existing == null) return false;
            existing.UserId = userId;
            existing.Description = transactionEditDto.Description;
            existing.Amount = transactionEditDto.Amount;
            existing.Type = transactionEditDto.Type;
            existing.Category = transactionEditDto.Category;
            existing.Date = transactionEditDto.Date;
            existing.Notes = transactionEditDto.Notes;
            await _repo.UpdateAsync(existing);
            return true;
        }


    }
}
