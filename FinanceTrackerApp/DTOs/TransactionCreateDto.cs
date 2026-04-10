using System.ComponentModel.DataAnnotations;

namespace FinanceTrackerApp.DTOs
{
    public class TransactionCreateDto
    {
        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Description must be 2-200 characters")]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Type { get; set; } = "Expense";
        [Required]
        [Range(0.01, 1_000_000, ErrorMessage = "Amount must be between 0.01 and 1,000,000.")]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;
        [StringLength(500)]
        public string? Notes {  get; set; }

    }
}
