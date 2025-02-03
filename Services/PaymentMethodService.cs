using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.PaymentMethod;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;

        public PaymentMethodService(ApplicationDbContext dbContext, MembershipNumberHelper membershipNumberHelper, IMapper mapper) 
        {
            _dbContext = dbContext;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
        }

        public async Task<List<PaymentMethodResponse>> GetAllPaymentMethods()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var paymentMethods = await _dbContext.PaymentMethods
                .Where(p => p.MembershipNumber == membershipNumber && p.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<List<PaymentMethodResponse>>(paymentMethods);
        }

        public async Task<PaymentMethodResponse?> GetPaymentMethodById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var paymentMethod = await _dbContext.PaymentMethods
                .Where(p => p.MembershipNumber == membershipNumber && p.IsDeleted == false && p.Id == id)
                .FirstOrDefaultAsync();

            if (paymentMethod is null) 
            {
                return null;
            }

            return _mapper.Map<PaymentMethodResponse>(paymentMethod);
        }

        public async Task<PaymentMethodResponse?> AddPaymentMethod(PaymentMethodAddRequest paymentMethodAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            PaymentMethod paymentMethod = new PaymentMethod() 
            {
                NameAr = paymentMethodAddRequest.NameAr,
                NameEn = paymentMethodAddRequest.NameEn,
                PaymentTypeId = paymentMethodAddRequest.PaymentTypeId,
                MembershipNumber = membershipNumber,
            };

            await _dbContext.PaymentMethods.AddAsync(paymentMethod);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PaymentMethodResponse>(paymentMethod);

        }

        public async Task<PaymentMethodResponse?> UpdatePaymentMethod(int id, PaymentMethodUpdateRequest paymentMethodUpdateRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var paymentMethod = await _dbContext.PaymentMethods
                .Where(p => p.MembershipNumber == membershipNumber && p.IsDeleted == false && p.Id == id)
                .FirstOrDefaultAsync();

            if (paymentMethod is null)
            {
                return null;
            }

            paymentMethod.NameAr = paymentMethodUpdateRequest.NameAr;
            paymentMethod.NameEn = paymentMethodUpdateRequest.NameEn;
            paymentMethod.PaymentTypeId = paymentMethodUpdateRequest.PaymentTypeId;
            paymentMethod.IsActive = paymentMethodUpdateRequest.IsActive;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PaymentMethodResponse>(paymentMethod);
        }

        public async Task<PaymentMethodResponse?> DeletePaymentMethod(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var paymentMethod = await _dbContext.PaymentMethods
                .Where(p => p.MembershipNumber == membershipNumber && p.IsDeleted == false && p.Id == id)
                .FirstOrDefaultAsync();

            if (paymentMethod is null)
            {
                return null;
            }

            
            _dbContext.PaymentMethods.Remove(paymentMethod);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PaymentMethodResponse>(paymentMethod);
        }
    }
}
