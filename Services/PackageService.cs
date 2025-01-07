using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Models.Dtos.Package;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class PackageService : IPackageService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PackageService(ApplicationDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<PackageResponse>> GetAllPackages()
        {
            var packages = await _dbContext.Packages.ToListAsync();

            return _mapper.Map<List<PackageResponse>>(packages);
        }

        public async Task<PackageResponse?> GetPackageById(int id)
        {
            var package = await _dbContext.Packages.FindAsync(id);

            if (package is null) 
            {
                return null;
            }

            return _mapper.Map<PackageResponse>(package);
        }

        public async Task<PackageResponse?> AddPackage(PackageAddRequest packageAddRequest)
        {
            Package package = new Package() 
            {
                NameAr = packageAddRequest.NameAr,
                NameEn = packageAddRequest.NameEn,
                Price = packageAddRequest.Price,
            };

            await _dbContext.AddAsync(package);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PackageResponse>(package);
        }

        public async Task<PackageResponse?> UpdatePackage(int id, PackageUpdateRequest packageUpdateRequest)
        {
            var package = await _dbContext.Packages.FindAsync(id);

            if (package is null)
            {
                return null;
            }

            package.NameAr = packageUpdateRequest.NameAr;
            package.NameEn = packageUpdateRequest.NameEn;
            package.Price = packageUpdateRequest.Price;
            package.IsActive = packageUpdateRequest.IsActive;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PackageResponse>(package);
        }

        public async Task<PackageResponse?> DeletePackage(int id)
        {
            var package = await _dbContext.Packages.FindAsync(id);

            if (package is null)
            {
                return null;
            }

           _dbContext.Packages.Remove(package);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PackageResponse>(package);
        }
    }
}
