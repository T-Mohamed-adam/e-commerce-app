using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Owner;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly PasswordHashingHelper _passwordHashingHelper;
        private readonly GenerateRandomNumberHelper _randomNumberHelper;

        public OwnerService(ApplicationDbContext dbContext, IMapper mapper, PasswordHashingHelper passwordHashingHelper, 
            GenerateRandomNumberHelper randomNumberHelper) 
        { 
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHashingHelper = passwordHashingHelper;
            _randomNumberHelper = randomNumberHelper;
        }

        public async Task<List<OwnerResponse>> GetAllOwners()
        {
            var owners = await _dbContext.Owners
                .Where(o => o.IsDeleted == false)
                .Include(o => o.Subscription)
                .ThenInclude(s => s.Package)
                .ToListAsync();

            return _mapper.Map<List<OwnerResponse>>(owners);
        }

        public async Task<OwnerResponse?> GetOwnerById(int id)
        {
            var owner = await _dbContext.Owners
                  .Where(o => o.IsDeleted == false)
                .Include(o => o.Subscription)
                .ThenInclude(s => s.Package)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (owner is null)
            {
                return null;
            }
            return _mapper.Map<OwnerResponse>(owner);
        }

        public async Task<OwnerResponse?> AddOwner(OwnerAddRequest ownerAddRequest)
        {
            Subscription subscription = new Subscription() 
            {
                PackageId = ownerAddRequest.PackageId,
                StartDate = ownerAddRequest.StartDate.ToUniversalTime(),
                EndDate = ownerAddRequest.EndDate.ToUniversalTime(),
            };
            await _dbContext.AddAsync(subscription);
            await _dbContext.SaveChangesAsync();


            Owner owner = new Owner() 
            {
                FirstName = ownerAddRequest.FirstName,
                LastName = ownerAddRequest.LastName,
                PhoneNumber = ownerAddRequest.PhoneNumber,
                Email = ownerAddRequest.Email,
                BusinessName = ownerAddRequest.BusinessName,
                Address = ownerAddRequest.Address,
                TIN = ownerAddRequest.TIN,
                CommercialRegister = ownerAddRequest.CommercialRegister,
                Password = _passwordHashingHelper.HashPassword(ownerAddRequest.Password!),
                CityId = ownerAddRequest.CityId,
                SubscriptionId = subscription.Id,
                MembershipNumber = _randomNumberHelper.GenerateMembershipNumber(),
            };

            await _dbContext.AddAsync(owner);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<OwnerResponse>(owner);
        }

        public async Task<OwnerResponse?> UpdateOwner(int id, OwnerUpdateRequest ownerUpdateRequest)
        {
            var owner = await _dbContext.Owners.FindAsync(id);
            if (owner is null)
            {
                return null;
            }

            owner.FirstName = ownerUpdateRequest.FirstName;
            owner.LastName = ownerUpdateRequest.LastName;
            owner.PhoneNumber = ownerUpdateRequest.PhoneNumber;
            owner.Email = ownerUpdateRequest.Email;
            owner.BusinessName = ownerUpdateRequest.BusinessName;
            owner.Address = ownerUpdateRequest.Address;
            owner.TIN = ownerUpdateRequest.TIN;
            owner.CommercialRegister = ownerUpdateRequest.CommercialRegister;
            owner.Password = _passwordHashingHelper.HashPassword(ownerUpdateRequest.Password!);
            owner.CityId = ownerUpdateRequest.CityId;
            owner.SubscriptionId = ownerUpdateRequest.SubscriptionId;

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<OwnerResponse>(owner);
        }

        public async Task<OwnerResponse?> DeleteOwner(int id)
        {
            var owner = await _dbContext.Owners.FindAsync(id);
            if (owner is null)
            {
                return null;
            }
            _dbContext.Owners.Remove(owner);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<OwnerResponse>(owner);
        }

        public string GenerateUniqueMembershipNumber()
        {
            string membershipNumber;
            bool exists;

            do
            {
                membershipNumber = _randomNumberHelper.GenerateMembershipNumber();
                exists = _dbContext.Owners.Any(o => o.MembershipNumber == membershipNumber);

            } while (exists);

            return membershipNumber;
        }

    }
}
