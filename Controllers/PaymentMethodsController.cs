using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.PaymentMethod;
using TagerProject.Models.Dtos.PaymentType;
using TagerProject.ServiceContracts;
using TagerProject.Services;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/paymentMethods
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]

    public class PaymentMethodsController : Controller
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodsController(IPaymentMethodService paymentMethodService) 
        {
            _paymentMethodService = paymentMethodService;
        }

        // Get all paymnet methods
        [HttpGet]
        public async Task<IActionResult> GetAllPaymentMethods()
        {
            var paymentMethods = await _paymentMethodService.GetAllPaymentMethods();

            return Ok(paymentMethods);

        }

        // Get specific payment method by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPaymentMethodById(int id)
        {
            var paymentMethod = await _paymentMethodService.GetPaymentMethodById(id);

            if (paymentMethod is null)
            {
                return NotFound("Payment method not found");
            }

            return Ok(paymentMethod);
        }

        // Add new payment method
        [HttpPost]
        public async Task<IActionResult> AddPaymentMethod(PaymentMethodAddRequest paymentMethodAddRequest)
        {
            try
            {
                var paymentMethod = await _paymentMethodService.AddPaymentMethod(paymentMethodAddRequest);

                return Ok(paymentMethod);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update specific payment method by its id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePaymentMethod(int id, PaymentMethodUpdateRequest paymentMethodUpdateRequest)
        {
            try
            {
                var paymentMethod = await _paymentMethodService.UpdatePaymentMethod(id, paymentMethodUpdateRequest);

                if (paymentMethod is null)
                {
                    return NotFound("Payment method not found");
                }

                return Ok(paymentMethod);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete specific payment method by its id 
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            var paymentMethod = await _paymentMethodService.DeletePaymentMethod(id);

            if (paymentMethod is null)
            {
                return NotFound("Payment method not found");
            }

            return Ok("Payment method deleted successfully");
        }
    }
}
