using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Coupon;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/coupons
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponsController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponsController(ICouponService couponService) 
        {
            _couponService = couponService;
        }

        // Get all coupons list
        [HttpGet]
        public async Task<IActionResult> GetAllCoupons()
        {
            var coupons = await _couponService.GetAllCoupons();

            return Ok(coupons);
        }

        // Get specific coupon by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCouponById(int id)
        {
            var coupon = await _couponService.GetCouponById(id);

            if (coupon is null)
            {
                return NotFound("Coupon not found");
            }

            return Ok(coupon);
        }

        // Add new coupon
        [HttpPost]
        public async Task<IActionResult> AddCoupon(CouponAddRequest couponAddRequest)
        {
            try
            {
                var coupon = await _couponService.AddCoupon(couponAddRequest);

                return Ok(coupon);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update specific coupon by its id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCoupon(int id, CouponUpdateRequest couponUpdateRequest)
        {
            try
            {
                var coupon = await _couponService.UpdateCoupon(id, couponUpdateRequest);

                if (coupon is null)
                {
                    return NotFound("Coupon not found");

                }
                return Ok(coupon);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Detele specific coupon by its id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var coupon = await _couponService.DeleteCoupon(id);

            if (coupon is null)
            {
                return NotFound("Coupon not found");
            }

            return Ok("Coupon deleted successfully");
        }
    }
}
