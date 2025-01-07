using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Expense;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/expenses
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly IExpenseService _expenseService;
        public ExpensesController(IExpenseService expenseService) 
        {
            _expenseService = expenseService;
        }

        // Get all expenses list
        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var expenses = await _expenseService.GetAllExpenses();

            return Ok(expenses);
        }

        // Get specific expense by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            var expense = await _expenseService.GetExpenseById(id);

            if (expense is null)
            {
                return NotFound("Expense not found");
            }

            return Ok(expense);
        }

        // Add new expense
        [HttpPost]
        public async Task<IActionResult> AddExpense(ExpenseAddRequest expenseAddRequest)
        {
            try
            {
                var expense = await _expenseService.AddExpense(expenseAddRequest);

                return Ok(expense);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update specific expense by its id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateExpenseItem(int id, ExpenseUpdateRequest expenseUpdateRequest)
        {
            try
            {
                var expense = await _expenseService.UpdateExpense(id, expenseUpdateRequest);

                if (expense is null)
                {
                    return NotFound("Expense not found");

                }
                return Ok(expense);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Detele specific expense by its id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _expenseService.DeleteExpense(id);

            if (expense is null)
            {
                return NotFound("Expense not found");
            }

            return Ok("Expense deleted successfully");
        }
    }
}
