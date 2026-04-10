using System.ComponentModel.DataAnnotations;

namespace FinanceTrackerApp.DTOs
{
    public class TransactionEditDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Type { get; set; } = "Expense";
        [Required]
        [Range(0.01, 1_000_000)]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
