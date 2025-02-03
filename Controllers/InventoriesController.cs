using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/inventories
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoriesController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public InventoriesController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // Get all Inventories for specific owner
        [HttpGet]
        public async Task<IActionResult> GetAllInventories()
        {
            var inventories = await _inventoryService.GetAllInventories();
            return Ok(inventories);
        }

        // Get inventory by id for specific owner
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInventoryById(int id)
        {
            var inventory = await _inventoryService.GetInventyById(id);

            if (inventory is null)
            {
                return NotFound("Inventory not found");
            }

            return Ok(inventory);
        }

        // Delete specific inventory data 
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteInventory(int id) 
        {
            var inventory = await _inventoryService.DeleteInventory(id);

            if (inventory is null)
            {
                return NotFound("Inventory not found");
            }

            return Ok("The inventory data deleted successfully");
        }
    }
}
