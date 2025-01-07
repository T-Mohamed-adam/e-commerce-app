using TagerProject.Models.Dtos.Expense;

namespace TagerProject.ServiceContracts
{
    public interface IExpenseService
    {
        public Task<List<ExpenseResponse>> GetAllExpenses();

        public Task<ExpenseResponse?> GetExpenseById(int id);

        public Task<ExpenseResponse?> AddExpense(ExpenseAddRequest expenseAddRequest);

        public Task<ExpenseResponse?> UpdateExpense(int id, ExpenseUpdateRequest expenseUpdateRequest);

        public Task<ExpenseResponse?> DeleteExpense(int id);
    }
}
