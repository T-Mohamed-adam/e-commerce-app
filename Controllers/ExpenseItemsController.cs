using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Expense;
using TagerProject.Models.Dtos.ExpenseItem;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/expenseItems
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseItemsController : Controller
    {
        private readonly IExpenseItemService _expenseItemService;

        public ExpenseItemsController(IExpenseItemService expenseItemService) 
        {
            _expenseItemService = expenseItemService;
        }

        // Get all expense items list
        [HttpGet]
        public async Task<IActionResult> GetAllExpenseItems()
        {
            var expenseItems = await _expenseItemService.GetAllExpenseItems();

            return Ok(expenseItems);
        }

        // Get specific  expense item by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetExpenseItemById(int id) 
        {
            var expenseItem = await _expenseItemService.GetExpenseItemById(id);

            if (expenseItem is null) 
            {
                return NotFound("Expense item not found");
            }

            return Ok(expenseItem);
        }

        // Add new expense item
        [HttpPost]
        public async Task<IActionResult> AddExpenseItem(ExpenseItemAddRequest expenseItemAddRequest) 
        {
            try
            {
                var expenseItem = await _expenseItemService.AddExpenseItem(expenseItemAddRequest);

                return Ok(expenseItem);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // Update specific expense item by its id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateExpenseItem(int id, ExpenseItemUpdateRequest expenseItemUpdateRequest) 
        {
            try
            {
                var expenseItem = await _expenseItemService.UpdateExpenseItem(id, expenseItemUpdateRequest);
                
                if (expenseItem is null) 
                {
                    return NotFound("Expense item not found");

                }
                return Ok(expenseItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Detele specific expense item by its id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteExpenseItem(int id) 
        {
            var expenseItem = await _expenseItemService.DeleteExpenseItem(id);

            if (expenseItem is null)
            {
                return NotFound("Expense item not found");
            }

            return Ok("Expense item deleted successfully");
        }
    }
}
