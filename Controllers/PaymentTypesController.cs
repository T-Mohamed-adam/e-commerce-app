using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.PaymentType;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/paymentTypes
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentTypesController : Controller
    {
        private readonly IPaymentTypeService _paymentTypeService;

        public PaymentTypesController(IPaymentTypeService paymentTypeService) 
        {
            _paymentTypeService = paymentTypeService;
        }

        // Get all paymnet types
        [HttpGet]
        public async Task<IActionResult> GetAllPaymentTypes()
        {
            var paymentTypes = await _paymentTypeService.GetAllPaymentTypes();

            return Ok(paymentTypes);

        }

        // Get specific payment type by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPaymentTypeById(int id) 
        {
            var paymentType = await _paymentTypeService.GetPaymentTypeById(id);

            if (paymentType is null) 
            {
                return NotFound("Payment type not found");
            }

            return Ok(paymentType);
        }

        // Add new payment type
        [HttpPost]
        public async Task<IActionResult> AddPaymentType(PaymentTypeAddRequest paymentTypeAddRequest) 
        {
            try
            {
                var paymentType = await _paymentTypeService.AddPaymentType(paymentTypeAddRequest);

                return Ok(paymentType);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // Update specific payment type by its id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpadtePaymentType(int id, PaymentTypeUpdateRequest paymentTypeUpdateRequest) 
        {
            try
            {
                var paymentType = await _paymentTypeService.UpdatePaymentType(id, paymentTypeUpdateRequest);

                if (paymentType is null)
                {
                    return NotFound("Payment type not found");
                }

                return Ok(paymentType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete specific payment type by its id 
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePaymentType(int id)
        {
            var paymentType = await _paymentTypeService.DeletePaymentType(id);

            if (paymentType is null)
            {
                return NotFound("Payment type not found");
            }

            return Ok("Payment type deleted successfully");
        }
    }
}
