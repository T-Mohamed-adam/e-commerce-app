using TagerProject.Models.Dtos.ExpenseItem;

namespace TagerProject.ServiceContracts
{
    public interface IExpenseItemService
    {
        public Task<List<ExpenseItemResponse>> GetAllExpenseItems();

        public Task<ExpenseItemResponse?> GetExpenseItemById(int id);

        public Task<ExpenseItemResponse?> AddExpenseItem(ExpenseItemAddRequest expenseItemAddRequest);

        public Task<ExpenseItemResponse?> UpdateExpenseItem(int id, ExpenseItemUpdateRequest expenseItemUpdateRequest);

        public Task<ExpenseItemResponse?> DeleteExpenseItem(int id);
    }
}
