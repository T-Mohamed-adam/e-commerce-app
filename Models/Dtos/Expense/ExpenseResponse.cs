using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.Expense
{
    public class ExpenseResponse
    {
        public int Id { get; set; }
        public int ExpenseItemId { get; set; }
        public string? ExpenseName { get; set; }
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
        public string? MembershipNumer { get; set; }
        public string? Status { get; set; }
        public bool Paid { get; set; }

        public virtual TagerProject.Models.Entities.Expense? Expense { get; set; }
    }
}
