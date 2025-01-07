using TagerProject.Models.Dtos.Brand;

namespace TagerProject.ServiceContracts
{
    public interface IBrandService
    {
        public Task<List<BrandResponse>> GetAllBrands();
        public Task<BrandResponse?> GetBrandById(int id);
        public Task<BrandResponse?> AddBrand(BrandAddRequest brandAddRequest);
        public Task<BrandResponse?> UpdateBrand(int id, BrandUpdateRequest brandUpdateRequest);
        public Task<BrandResponse?> DeleteBrand(int id);
    }
}
