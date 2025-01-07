using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Entities
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ExpenseItemId { get; set; }
        [Required]
        public string? ExpenseName { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
        [Required]
        public string? MembershipNumer { get; set; }
        public string Status { get; set; } = "Pending";
        public bool Paid { get; set; } = false;
        public virtual ExpenseItem? ExpenseItem { get; set; }
    }
}
