using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Pagination;
using TagerProject.Models.Dtos.Purchase;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/purchases
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]

    public class PurchasesController : Controller
    {
        private readonly IPurchaseService _purchaseService;

        public PurchasesController(IPurchaseService purchaseService) 
        {
            _purchaseService = purchaseService;
        }

        // Get all purchases list
        [HttpGet]
        public async Task<IActionResult> GetAllPurchases()
        {
            var purchases = await _purchaseService.GetAllPurchases();
            return Ok(purchases);
        }

        // Get all purchases list
        [HttpGet("list")]
        public async Task<IActionResult> GetPurchasesList([FromQuery] PaginationRequest request)
        {
            var purchases = await _purchaseService.GetPurchasesList(request);
            return Ok(purchases);
        }

        // Get specific purchase
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPurchaseById(int id)
        {
            var purchase = await _purchaseService.GetPurchaseById(id);
            
            if (purchase is null) 
            {
                return NotFound("Purchase not found");
            }

            return Ok(purchase);
        }

        // Add new purchase operation
        [HttpPost]
        public async Task<IActionResult> AddPurchase(PurchaseAddRequest purchaseAddRequest) 
        {
            try
            {
                var purchase = await _purchaseService.AddPurchase(purchaseAddRequest);

                if (purchase is null) 
                {
                    return BadRequest("Unable to add purchase operation");
                }

                return Ok(purchase);

            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete specific purchase
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var purchase = await _purchaseService.DeletePurchase(id);

            if (purchase is null)
            {
                return NotFound("Purchase not found");
            }

            return Ok("The purchase data deleted successfully");
        }
    }
}
