using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Unit;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class UnitService : IUnitService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;

        public UnitService(ApplicationDbContext dbContext, MembershipNumberHelper membershipNumberHelper, IMapper mapper) 
        {
            _dbContext = dbContext;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
        }

        public async Task<List<UnitResponse>> GetAllUnites()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();
            
            var units = await _dbContext.Units
                .Where(u => u.MembershipNumber == membershipNumber && u.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<List<UnitResponse>>(units);
        }

        public async Task<UnitResponse?> GetUnitById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var unit = await _dbContext.Units
                .Where(u => u.MembershipNumber == membershipNumber && u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (unit is null) 
            {
                return null;
            }

            return _mapper.Map<UnitResponse>(unit);
        }

        public async Task<UnitResponse?> AddUnit(UnitAddRequest unitAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            Unit unit = new Unit() 
           {
               NameAr = unitAddRequest.NameAr,
               NameEn = unitAddRequest.NameEn,
               MembershipNumber = membershipNumber,
           };

            await _dbContext.Units.AddAsync(unit);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UnitResponse>(unit);

        }

        public async Task<UnitResponse?> UpdateUnit(int id, UnitUpdateRequest unitUpdateRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var unit = await _dbContext.Units
                .Where(u => u.MembershipNumber == membershipNumber && u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (unit is null)
            {
                return null;
            }

            unit.NameAr = unitUpdateRequest.NameAr;
            unit.NameEn = unitUpdateRequest.NameEn;
            unit.IsActive = unitUpdateRequest.IsActive;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UnitResponse>(unit);
        }

        public async Task<UnitResponse?> DeleteUnit(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var unit = await _dbContext.Units
                .Where(u => u.MembershipNumber == membershipNumber && u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (unit is null)
            {
                return null;
            }

            _dbContext.Units.Remove(unit);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UnitResponse>(unit);
        }
    }
}
