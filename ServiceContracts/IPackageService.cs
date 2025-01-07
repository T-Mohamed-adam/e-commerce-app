using TagerProject.Models.Dtos.Package;

namespace TagerProject.ServiceContracts
{
    public interface IPackageService
    {
        public Task<List<PackageResponse>> GetAllPackages();

        public Task<PackageResponse?> GetPackageById(int id);

        public Task<PackageResponse?> AddPackage(PackageAddRequest packageAddRequest);

        public Task<PackageResponse?> UpdatePackage(int id, PackageUpdateRequest packageUpdateRequest);

        public Task<PackageResponse?> DeletePackage(int id);
    }
}
