using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTrackerApp.Models
{
    public class Transaction
    {
        public int Id { get; set;  }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        public string Category {  get; set; } = string.Empty;
        [Required]
        public string Type { get; set; } = "Expense";
        [Required]
        public DateTime Date {  get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }

    }
}
