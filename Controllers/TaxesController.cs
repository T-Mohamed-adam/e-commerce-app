using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Tax;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/taxes
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TaxesController : Controller
    {
        private readonly ITaxService _taxService;

        public TaxesController(ITaxService taxService) 
        {
            _taxService = taxService;
        }

        // Get all taxes
        [HttpGet]
        public async Task<IActionResult> GetAllTaxes()
        {
            var taxes = await _taxService.GetAllTaxes();
            return Ok(taxes);
        }

        // Get specific tax by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTaxById(int id)
        {
            var tax = await _taxService.GetTaxById(id);

            if (tax is null)
            {
                return NotFound("Tax not found");
            }
            return Ok(tax);
        }

        // Add new tax
        [HttpPost]
        public async Task<IActionResult> AddTax([FromForm] TaxAddRequest taxAddRequest)
        {
            try
            {
                var tax = await _taxService.AddTax(taxAddRequest);
                return Ok(tax);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update specific tax by its id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTax(int id, [FromForm] TaxUpdateRequest taxUpdateRequest)
        {
            try
            {
                var tax = await _taxService.UpdateTax(id, taxUpdateRequest);

                if (tax is null)
                {
                    return NotFound("Tax not found");
                }

                return Ok(tax);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete specific tax by its id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTax(int id)
        {
            var tax = await _taxService.DeleteTax(id);

            if (tax is null)
            {
                return NotFound("Tax not found");
            }
            return Ok("Tax data deleted successfully");
        }
    }
}
