namespace TagerProject.Models.Dtos.Expense
{
    public class ExpenseAddRequest
    {
        public int ExpenseItemId { get; set; }
        public string? ExpenseName { get; set; }
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
        public bool Paid { get; set; }
    }
}
