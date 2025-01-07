using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Discount;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;

        public DiscountService(ApplicationDbContext dbContext, MembershipNumberHelper membershipNumberHelper, IMapper mapper) 
        {
            _dbContext = dbContext;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
        }

        public async Task<List<DiscountResponse>> GetAllDiscounts()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var discounts = await _dbContext.Discounts
                .Where(d => d.MembershipNumber == membershipNumber && d.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<List<DiscountResponse>>(discounts);
        }

        public async Task<DiscountResponse?> GetDiscountById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var discount = await _dbContext.Discounts
                .Where(d => d.MembershipNumber == membershipNumber && d.IsDeleted == false && d.Id == id)
                .FirstOrDefaultAsync();

            if (discount is null) 
            {
                return null;
            }

            return _mapper.Map<DiscountResponse>(discount);
        }

        public async Task<DiscountResponse?> AddDiscount(DiscountAddRequest discountAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            Discount discount = new Discount() 
            {
                NameAr = discountAddRequest.NameAr,
                NameEn = discountAddRequest.NameEn,
                Value = discountAddRequest.Value,
                StartDate = discountAddRequest.StartDate,
                EndDate = discountAddRequest.EndDate,
                MembershipNumber = membershipNumber
            };
            await _dbContext.Discounts.AddAsync(discount);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<DiscountResponse>(discount);
        }

        public async Task<DiscountResponse?> UpdateDiscount(int id, DiscountUpdateRequest discountUpdateRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var discount = await _dbContext.Discounts
                .Where(d => d.MembershipNumber == membershipNumber && d.IsDeleted == false && d.Id == id)
                .FirstOrDefaultAsync();

            if (discount is null)
            {
                return null;
            }

            discount.NameAr = discountUpdateRequest.NameAr;
            discount.NameEn = discountUpdateRequest.NameEn;
            discount.Value = discountUpdateRequest.Value;
            discount.StartDate = discountUpdateRequest.StartDate;
            discount.EndDate = discountUpdateRequest.EndDate;
            discount.IsActive = discountUpdateRequest.IsActive;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<DiscountResponse>(discount);
        }

        public async Task<DiscountResponse?> DeleteDiscount(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var discount = await _dbContext.Discounts
                .Where(d => d.MembershipNumber == membershipNumber && d.IsDeleted == false && d.Id == id)
                .FirstOrDefaultAsync();

            if (discount is null)
            {
                return null;
            }

            _dbContext.Discounts.Remove(discount);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<DiscountResponse>(discount);
        }

    }
}
