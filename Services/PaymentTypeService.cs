using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.PaymentType;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;

        public PaymentTypeService(ApplicationDbContext dbContext, MembershipNumberHelper membershipNumberHelper, IMapper mapper) 
        {
            _dbContext = dbContext;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
        }
        public async Task<List<PaymentTypeResponse>> GetAllPaymentTypes()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var paymentTypes = await _dbContext.PaymentTypes
                .Where(p => p.MembershipNumber == membershipNumber && p.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<List<PaymentTypeResponse>>(paymentTypes);
        }

        public async Task<PaymentTypeResponse?> GetPaymentTypeById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var paymentType = await _dbContext.PaymentTypes
                .Where(p => p.MembershipNumber == membershipNumber && p.IsDeleted == false && p.Id == id)
                .FirstOrDefaultAsync();

            if (paymentType is null) 
            {
                return null;
            }

            return _mapper.Map<PaymentTypeResponse>(paymentType);
        }

        public async Task<PaymentTypeResponse?> AddPaymentType(PaymentTypeAddRequest paymentTypeAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            PaymentType paymentType = new PaymentType() 
            {
                NameAr = paymentTypeAddRequest.NameAr,
                NameEn = paymentTypeAddRequest?.NameEn,
                MembershipNumber = membershipNumber,
            };

            await _dbContext.PaymentTypes.AddAsync(paymentType);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PaymentTypeResponse>(paymentType);
        }
        public async Task<PaymentTypeResponse?> UpdatePaymentType(int id, PaymentTypeUpdateRequest paymentTypeUpdateRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var paymentType = await _dbContext.PaymentTypes
                .Where(p => p.MembershipNumber == membershipNumber && p.IsDeleted == false && p.Id == id)
                .FirstOrDefaultAsync();

            if (paymentType is null)
            {
                return null;
            }

            paymentType.NameAr = paymentTypeUpdateRequest.NameAr;
            paymentType.NameEn = paymentTypeUpdateRequest.NameEn;
            paymentType.IsActive = paymentTypeUpdateRequest.IsActive;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PaymentTypeResponse>(paymentType);
        }

        public async Task<PaymentTypeResponse?> DeletePaymentType(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var paymentType = await _dbContext.PaymentTypes
                .Where(p => p.MembershipNumber == membershipNumber && p.IsDeleted == false && p.Id == id)
                .FirstOrDefaultAsync();

            if (paymentType is null)
            {
                return null;
            }

            _dbContext.PaymentTypes.Remove(paymentType);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PaymentTypeResponse>(paymentType);
        }

    }
}
