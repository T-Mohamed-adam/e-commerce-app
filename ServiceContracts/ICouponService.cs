using TagerProject.Models.Dtos.Coupon;
using TagerProject.Models.Dtos.Discount;

namespace TagerProject.ServiceContracts
{
    public interface ICouponService
    {
        public Task<List<CouponResponse>> GetAllCoupons();

        public Task<CouponResponse?> GetCouponById(int id);

        public Task<CouponResponse?> AddCoupon(CouponAddRequest couponAddRequest);

        public Task<CouponResponse?> UpdateCoupon(int id, CouponUpdateRequest couponUpdateRequest);

        public Task<CouponResponse?> DeleteCoupon(int id);
    }
}
