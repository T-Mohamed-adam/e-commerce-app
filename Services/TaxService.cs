using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Tax;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class TaxService : ITaxService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;

        public TaxService(ApplicationDbContext dbContext, MembershipNumberHelper membershipNumberHelper, IMapper mapper) 
        {
            _dbContext = dbContext;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
        }

        public async Task<List<TaxResponse>> GetAllTaxes()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var taxes = await _dbContext.Taxes
                .Where(t => t.MembershipNumber == membershipNumber && t.IsDeleted == false)
                .ToListAsync();
            return _mapper.Map<List<TaxResponse>>(taxes);
        }

        public async Task<TaxResponse?> GetTaxById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var tax = await _dbContext.Taxes
                .Where(t => t.MembershipNumber == membershipNumber && t.IsDeleted == false)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tax is null) 
            {
                return null;
            }

            return _mapper.Map<TaxResponse>(tax);
        }

        public async Task<TaxResponse?> AddTax(TaxAddRequest taxAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            Tax tax = new Tax() 
            {
                NameAr = taxAddRequest.NameAr,
                NameEn = taxAddRequest.NameEn,
                Value = taxAddRequest.Value,
                MembershipNumber = membershipNumber
            };

            await _dbContext.Taxes.AddAsync(tax);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TaxResponse>(tax);
        }

        public async Task<TaxResponse?> UpdateTax(int id, TaxUpdateRequest taxUpdateRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var tax = await _dbContext.Taxes
                .Where(t => t.MembershipNumber == membershipNumber && t.IsDeleted == false)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tax is null)
            {
                return null;
            }

            tax.NameAr = taxUpdateRequest.NameAr;
            tax.NameEn = taxUpdateRequest.NameEn;
            tax.Value = taxUpdateRequest.Value;
            tax.IsActive = taxUpdateRequest.IsActive;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TaxResponse>(tax);
        }

        public async Task<TaxResponse?> DeleteTax(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var tax = await _dbContext.Taxes
                .Where(t => t.MembershipNumber == membershipNumber && t.IsDeleted == false)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tax is null)
            {
                return null;
            }

            _dbContext.Taxes.Remove(tax);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TaxResponse>(tax);
        }
    }
}
