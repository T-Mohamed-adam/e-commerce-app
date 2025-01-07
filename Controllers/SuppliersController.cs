using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Supplier;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/suppliers
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService) 
        {
            _supplierService = supplierService;
        }

        // Get all suppliers
        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await _supplierService.GetAllSuppliers();

            return Ok(suppliers);
        }

        // Get supplier by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            var supplier = await _supplierService.GetSupplierById(id);
            if (supplier is null)
            {
                return NotFound("Supplier not found");
            }
            return Ok(supplier);
        }

        // Add new supplier
        [HttpPost]
        public async Task<IActionResult> AddSupplier(SupplierAddRequest supplierAddRequest)
        {
            try
            {
                var supplier = await _supplierService.AddSupplier(supplierAddRequest);
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // Update specific supplier data
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSupplier(int id, SupplierUpdateRequest supplierUpdateRequest)
        {
            try
            {
                var supplier = await _supplierService.UpdateSupplier(id, supplierUpdateRequest);
                if (supplier is null)
                {
                    return NotFound("Supplier not found");
                }
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Update specific supplier data
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = await _supplierService.DeleteSupplier(id);
            if (supplier is null)
            {
                return NotFound("Supplier not found");
            }
            return Ok("Supplier data deleted successfully");
        }
    }
}
