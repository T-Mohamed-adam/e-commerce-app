using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Brand;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly FileHelper _fileHelper;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;

        public BrandService(ApplicationDbContext dbContext, FileHelper fileHelper,
            MembershipNumberHelper membershipNumberHelper, IMapper mapper)
        {
            _dbContext = dbContext;
            _fileHelper = fileHelper;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
        }

        public async Task<List<BrandResponse>> GetAllBrands()
        {
           var membershipNumber = _membershipNumberHelper.GetMembershipNumber();
            var brands = await _dbContext.Brands
                .Where(b => b.MembershipNumber == membershipNumber && b.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<List<BrandResponse>>(brands);
        }

        public async Task<BrandResponse?> GetBrandById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var brand = await _dbContext.Brands
                .Where(b => b.MembershipNumber == membershipNumber && b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (brand is null) 
            {
                return null;
            }

            return _mapper.Map<BrandResponse>(brand);
        }

        public async Task<BrandResponse?> AddBrand(BrandAddRequest brandAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            Brand brand = new Brand() 
            {
                NameAr = brandAddRequest.NameAr,
                NameEn = brandAddRequest.NameEn,
                Description = brandAddRequest.Description,
                MembershipNumber = membershipNumber,
            };

            if (brandAddRequest.Image != null) 
            {
                brand.ImageUrl = await _fileHelper.SaveImageAsync(brandAddRequest.Image);
            }

            await _dbContext.AddRangeAsync(brand);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BrandResponse>(brand);
        }

        public async Task<BrandResponse?> UpdateBrand(int id, BrandUpdateRequest brandUpdateRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var brand = await _dbContext.Brands
                .Where(b => b.MembershipNumber == membershipNumber && b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (brand is null)
            {
                return null;
            }

            brand.NameAr = brandUpdateRequest.NameAr;
            brand.NameEn = brandUpdateRequest.NameEn;
            brand.Description = brandUpdateRequest.Description;
            brand.IsActive = brandUpdateRequest.IsActive;

            if (brandUpdateRequest.Image != null)
            {
                brand.ImageUrl = await _fileHelper.SaveImageAsync(brandUpdateRequest.Image);
            }

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BrandResponse>(brand);
        }

        public async Task<BrandResponse?> DeleteBrand(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var brand = await _dbContext.Brands
                .Where(b => b.MembershipNumber == membershipNumber && b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (brand is null)
            {
                return null;
            }

            _dbContext.Brands.Remove(brand);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BrandResponse>(brand);
        }
    }
}
