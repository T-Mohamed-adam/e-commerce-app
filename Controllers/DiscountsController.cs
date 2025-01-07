using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Coupon;
using TagerProject.Models.Dtos.Discount;
using TagerProject.ServiceContracts;
using TagerProject.Services;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/discounts
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class DiscountsController : Controller
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService) 
        {
            _discountService = discountService;
        }


        // Get all discounts list
        [HttpGet]
        public async Task<IActionResult> GetAllDiscounts()
        {
            var discounts = await _discountService.GetAllDiscounts();

            return Ok(discounts);
        }

        // Get specific discount by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDiscountById(int id)
        {
            var discount = await _discountService.GetDiscountById(id);

            if (discount is null)
            {
                return NotFound("Discount not found");
            }

            return Ok(discount);
        }

        // Add new discount
        [HttpPost]
        public async Task<IActionResult> AddDiscount(DiscountAddRequest discountAddRequest)
        {
            try
            {
                var discount = await _discountService.AddDiscount(discountAddRequest);

                return Ok(discount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update specific discount by its id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDiscount(int id, DiscountUpdateRequest discountUpdateRequest)
        {
            try
            {
                var discount = await _discountService.UpdateDiscount(id, discountUpdateRequest);

                if (discount is null)
                {
                    return NotFound("Discount not found");

                }
                return Ok(discount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Detele specific discount by its id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var discount = await _discountService.DeleteDiscount(id);

            if (discount is null)
            {
                return NotFound("Discount not found");
            }

            return Ok("Discount deleted successfully");
        }
    }
}
