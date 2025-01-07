using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Coupon;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class CouponService : ICouponService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;

        public CouponService( ApplicationDbContext dbContext, MembershipNumberHelper membershipNumberHelper, IMapper mapper)
        {
            _dbContext = dbContext;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
        }

        public async Task<List<CouponResponse>> GetAllCoupons()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var coupons = await _dbContext.Coupons
                .Where(c => c.MembershipNumber == membershipNumber && c.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<List<CouponResponse>>(coupons);
        }

        public async Task<CouponResponse?> GetCouponById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var coupon = await _dbContext.Coupons
                .Where(c => c.MembershipNumber == membershipNumber && c.IsDeleted == false && c.Id == id)
                .FirstOrDefaultAsync();

            if (coupon is null) 
            {
                return null;
            }

            return _mapper.Map<CouponResponse>(coupon);
        }

        public async Task<CouponResponse?> AddCoupon(CouponAddRequest couponAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            Coupon coupon = new Coupon() 
            {
                CouponCode = couponAddRequest.CouponCode,
                Value = couponAddRequest.Value,
                StartDate = couponAddRequest.StartDate,
                EndDate = couponAddRequest.EndDate,
                MembershipNumber = membershipNumber
            };

            await _dbContext.AddRangeAsync(coupon);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CouponResponse>(coupon);
        }

        public async Task<CouponResponse?> UpdateCoupon(int id, CouponUpdateRequest couponUpdateRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var coupon = await _dbContext.Coupons
                .Where(c => c.MembershipNumber == membershipNumber && c.IsDeleted == false && c.Id == id)
                .FirstOrDefaultAsync();

            if (coupon is null)
            {
                return null;
            }

            coupon.CouponCode = couponUpdateRequest.CouponCode;
            coupon.Value = couponUpdateRequest.Value;
            coupon.StartDate = couponUpdateRequest.StartDate;
            coupon.EndDate = couponUpdateRequest.EndDate;
            coupon.IsActive = couponUpdateRequest.IsActive;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CouponResponse>(coupon);
        }

        public async Task<CouponResponse?> DeleteCoupon(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var coupon = await _dbContext.Coupons
                .Where(c => c.MembershipNumber == membershipNumber && c.IsDeleted == false && c.Id == id)
                .FirstOrDefaultAsync();

            if (coupon is null)
            {
                return null;
            }

            _dbContext.Coupons.Remove(coupon);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CouponResponse>(coupon);
        }

    }
}
