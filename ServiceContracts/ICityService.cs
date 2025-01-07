using TagerProject.Models.Dtos.City;

namespace TagerProject.ServiceContracts
{
    public interface ICityService
    {
        public Task<List<CityResponse>> GetAllCity();

        public Task<CityResponse?> GetCityById(int id);

        public Task<CityResponse?> AddCity(CityAddRequest cityAddRequest);

        public Task<CityResponse?> UpdateCity(int id, CityUpdateRequest cityUpdateRequest);

        public Task<CityResponse?> DeleteCity(int id);


    }
}
